using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using dytsenayasar.Models.Settings;
using dytsenayasar.Services.Abstract;
using Microsoft.AspNetCore.Http;
using dytsenayasar.DataAccess.Entities;
using dytsenayasar.Context;

namespace dytsenayasar.Services.Concrete
{
    public class FileManager : IFileManager
    {
        private const string THUMBNAIL_POSTFIX = "_thumb";
        private readonly ILogger _logger;
        private readonly FileManagerSettings _settings;
        private readonly ApplicationDbContext _context;

        public FileManager(ILogger<FileManager> logger, IOptions<FileManagerSettings> settings, ApplicationDbContext context)
        {
            _logger = logger;
            _settings = settings.Value;
            _context = context;
        }

        public Task<FileManagerResult> ReadFile(string fileName)
        {
            return ReadFile(_settings.FilePath, fileName);
        }

        public FileManagerResult OpenFileStream(string fileName)
        {
            return OpenFileStream(_settings.FilePath, fileName);
        }

        public Task<FileManagerResult> ReadImage(string fileName)
        {
            return ReadFile(_settings.FilePath, fileName);
        }

        public FileManagerResult OpenImageStream(string fileName)
        {
            return OpenFileStream(_settings.FilePath, fileName);
        }

        public async Task<FileManagerResult> ReadFile(string path, string fileName)
        {
            var result = new FileManagerResult { Name = fileName };
            try
            {
                using (var file = new FileStream(Path.Combine(path, fileName), FileMode.Open, FileAccess.Read,
                    FileShare.Read, bufferSize: 4096, useAsync: true))
                {
                    byte[] buffer = new byte[0x1000];

                    int numRead;
                    using (var stream = new MemoryStream())
                    {
                        while ((numRead = await file.ReadAsync(buffer, 0, buffer.Length)) != 0)
                        {
                            await stream.WriteAsync(buffer, 0, numRead);
                        }
                        await stream.FlushAsync();

                        result.Status = FileManagerStatus.Completed;
                        result.Data = stream.ToArray();
                        return result;
                    }
                }
            }
            catch (System.Security.SecurityException) { result.Status = FileManagerStatus.PermissionDenied; return result; }
            catch (FileNotFoundException) { result.Status = FileManagerStatus.FileNotFound; return result; }
            catch (DirectoryNotFoundException) { result.Status = FileManagerStatus.PathNotFound; return result; }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                result.Status = FileManagerStatus.Failed;
                return result;
            }
        }

        public FileManagerResult OpenFileStream(string path, string fileName)
        {
            var result = new FileManagerResult { Name = fileName };
            try
            {
                result.Stream = new FileStream(Path.Combine(path, fileName), FileMode.Open, FileAccess.Read,
                    FileShare.Read, bufferSize: 4096, useAsync: true);

                result.Status = FileManagerStatus.Completed;
                return result;
            }
            catch (System.Security.SecurityException) { result.Status = FileManagerStatus.PermissionDenied; return result; }
            catch (FileNotFoundException) { result.Status = FileManagerStatus.FileNotFound; return result; }
            catch (DirectoryNotFoundException) { result.Status = FileManagerStatus.PathNotFound; return result; }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                result.Status = FileManagerStatus.Failed;
                return result;
            }
        }

        public async Task<FileManagerResult> WriteFile(Guid userId, IFormFile file)
        {
            var result = new FileManagerResult { Name = file.Name };

            if (file == null)
            {
                result.Status = FileManagerStatus.Completed;
                return result;
            }

            if (file.Length / 1048576 > 100)
            {
                result.Status = FileManagerStatus.TooBigFile;
                return result;
            }

            if (file != null)
            {
                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    
                    var fileExtension = Path.GetExtension(fileName);
                    
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    var objfiles = new Content()
                    {
                        Name= newFileName,
                        FileType = fileExtension,
                        CreatedOn = DateTime.Now,
                        UserId = userId,
                    };
                    
                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        objfiles.DataFiles = target.ToArray();
                    }

                    await _context.Contents.AddAsync(objfiles);
                    await _context.SaveChangesAsync();
                    
                    result.Status = FileManagerStatus.Completed;
                }
                
            }
            return result;
        }

        public async Task<FileManagerResult> CreateImage(string imgName)
        {
            var result = new FileManagerResult { Name = imgName + THUMBNAIL_POSTFIX };
            var path = Path.Combine(_settings.FilePath, imgName);

            if (!File.Exists(path))
            {
                result.Status = FileManagerStatus.FileNotFound;
                return result;
            }

            return await Task.Factory.StartNew(() =>
            {
                using (var image = Image.Load(path))
                {
                    int w, h;
                    ScaleImageSize(image.Width, image.Height, _settings.ImagePixel, out w, out h);
                    image.Mutate(x => x.Resize(w, h));

                    image.Save(Path.Combine(_settings.FilePath, result.Name), new JpegEncoder() { Quality = 75 });
                    result.Status = FileManagerStatus.Completed;
                }
                return result;
            });
        }

        public async Task<FileManagerResult> ReadBytes(string fileName, int byteCount, int offset = 0, bool dispose = true)
        {
            var openResult = OpenFileStream(fileName);
            if (openResult.Status != FileManagerStatus.Completed) return openResult;

            return await ReadBytes(openResult.Stream, byteCount, offset, fileName, dispose);
        }

        public async Task<FileManagerResult> ReadBytes(Stream data, int byteCount, int offset = 0, string fileName = null, bool dispose = true)
        {
            var result = new FileManagerResult { Name = fileName };

            if (data == null || byteCount <= 0)
            {
                result.Status = FileManagerStatus.Completed;
                return result;
            }

            try
            {
                var buffer = new byte[byteCount];

                data.Position = offset;
                await data.ReadAsync(buffer, 0, buffer.Length);

                result.Data = buffer;
                result.Status = FileManagerStatus.Completed;
                return result;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                result.Status = FileManagerStatus.Failed;
                return result;
            }
            finally
            {
                if (dispose) data.Close();
                else data.Position = 0;
            }
        }

        public async Task<FileManagerResult> DeleteFile(string fileName)
        {
            var path = Path.Combine(_settings.FilePath, fileName);
            var result = new FileManagerResult { Name = fileName };
            if (!File.Exists(path))
            {
                result.Status = FileManagerStatus.FileNotFound;
                return result;
            }

            return await Task.Factory.StartNew(() =>
            {
                File.Delete(path);
                result.Status = FileManagerStatus.Completed;
                return result;
            });
        }

        public async Task<FileManagerResult> DeleteImage(string fileName)
        {
            var path = Path.Combine(_settings.FilePath, fileName);
            var result = new FileManagerResult { Name = fileName };
            if (!File.Exists(path))
            {
                result.Status = FileManagerStatus.FileNotFound;
                return result;
            }

            return await Task.Factory.StartNew(() =>
            {
                File.Delete(path);
                result.Status = FileManagerStatus.Completed;
                return result;
            });
        }

        public void ScaleImageSize(int width, int height, int targetSize, out int w, out int h)
        {
            double ratio = Math.Min(
                ((double)targetSize / width),
                ((double)targetSize / height)
            );

            w = (int)(width * ratio);
            h = (int)(height * ratio);
        }

    }
}