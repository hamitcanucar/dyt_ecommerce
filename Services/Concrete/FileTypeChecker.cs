using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using dytsenayasar.Services.Abstract;

namespace dytsenayasar.Services.Concrete
{
    public class FileTypeChecker : IFileTypeChecker
    {
        private readonly IFileManager _fileManager;

        public FileTypeChecker(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public async Task<bool> IsFileTypeCorrect(Stream stream, FileType fileType)
        {
            foreach (FileType flag in Enum.GetValues(typeof(FileType)))
            {
                if (fileType.HasFlag(flag))
                {
                    if (await CheckSingle(stream, flag)) return true;
                }
            }

            return false;
        }

        private async Task<bool> CheckSingle(Stream stream, FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Mp3: return await IsMp3(stream);
                case FileType.Avi: return await IsAvi(stream);
                case FileType._3gp: return await Is3gp(stream);
                case FileType.Flv: return await IsFlv(stream);
                case FileType.Mp4: return await IsMp4(stream);
                case FileType.Jpg: return await IsJpg(stream);
                case FileType.Gif: return await IsGif(stream);
                case FileType.Bmp: return await IsBmp(stream);
                case FileType.Png: return await IsPng(stream);
                case FileType.Epub: return await IsEpub(stream);
                case FileType.Docx: return await IsDocx(stream);
                case FileType.Doc: return await IsDoc(stream);
                case FileType.Pdf: return await IsPdf(stream);
                case FileType.Zip: return await IsZip(stream);
                default: return false;
            }
        }

        private async Task<bool> IsZip(Stream stream)
        {
            var data = await ReadBytes(stream, 4, 0);
            return !string.IsNullOrWhiteSpace(data) && (data == "50-4B-03-04" || data == "50-4B-05-06" || data == "50-4B-07-08");
        }

        private async Task<bool> IsPdf(Stream stream)
        {
            var data = await ReadBytes(stream, 4, 0);
            return !string.IsNullOrWhiteSpace(data) && (data == "25-50-44-46");
        }

        private async Task<bool> IsDoc(Stream stream)
        {
            bool result;

            var data = await ReadBytes(stream, 4, 0);
            result = !string.IsNullOrWhiteSpace(data) && (data == "0D-44-4F-43" || data == "DB-A5-2D-00");
            if (result) return true;

            data = await ReadBytes(stream, 8, 0);
            result = !string.IsNullOrWhiteSpace(data) && (data == "D0-CF-11-E0-A1-B1-1A-E1" || data == "CF-11-E0-A1-B1-1A-E1-00");
            if (result) return true;

            data = await ReadBytes(stream, 4, 512);
            return !string.IsNullOrWhiteSpace(data) && (data == "EC-A5-C1-00");
        }

        private async Task<bool> IsDocx(Stream stream)
        {
            var data = await ReadBytes(stream, 4, 0);
            return !string.IsNullOrWhiteSpace(data) && (data == "50-4B-03-04");
        }

        private async Task<bool> IsEpub(Stream stream)
        {
            var data = await ReadBytes(stream, 4, 0);
            return !string.IsNullOrWhiteSpace(data) && (data == "50-4B-03-04");
        }

        private async Task<bool> IsPng(Stream stream)
        {
            var data = await ReadBytes(stream, 8, 0);
            return !string.IsNullOrWhiteSpace(data) && (data == "89-50-4E-47-0D-0A-1A-0A");
        }

        private async Task<bool> IsBmp(Stream stream)
        {
            var data = await ReadBytes(stream, 2, 0);
            return !string.IsNullOrWhiteSpace(data) && (data == "42-4D");
        }

        private async Task<bool> IsGif(Stream stream)
        {
            var data = await ReadBytes(stream, 4, 0);
            return !string.IsNullOrWhiteSpace(data) && (data == "47-49-46-38");
        }

        private async Task<bool> IsJpg(Stream stream)
        {
            var data = await ReadBytes(stream, 4, 0);
            return !string.IsNullOrWhiteSpace(data) && (data == "FF-D8-FF-E0" || data == "FF-D8-FF-E1" || data == "FF-D8-FF-E8");
        }

        private async Task<bool> IsMp4(Stream stream)
        {
            var data = await ReadBytes(stream, 8, 4);
            return !string.IsNullOrWhiteSpace(data) && (data == "66-74-79-70-4D-53-4E-56" || data == "66-74-79-70-69-73-6F-6D"
                || data == "66-74-79-70-6D-70-34-32" || data == "66-74-79-70-4D-34-56-20");
        }

        private async Task<bool> IsFlv(Stream stream)
        {
            bool result;

            var data = await ReadBytes(stream, 3, 0);
            result = !string.IsNullOrWhiteSpace(data) && (data == "46-4C-56");
            if (result) return true;

            data = await ReadBytes(stream, 8, 4);
            return !string.IsNullOrWhiteSpace(data) && (data == "66-74-79-70-4D-34-56-20");
        }

        private async Task<bool> Is3gp(Stream stream)
        {
            bool result;

            var data = await ReadBytes(stream, 7, 4);
            result = !string.IsNullOrWhiteSpace(data) && (data == "66-74-79-70-33-67-70");
            if (result) return true;

            data = await ReadBytes(stream, 8, 0);
            return !string.IsNullOrWhiteSpace(data) && (data == "00-00-00-14-66-74-79-70" || data == "00-00-00-20-66-74-79-70"
                || data == "00-00-00-18-66-74-79-70");
        }

        private async Task<bool> IsAvi(Stream stream)
        {
            var data = await ReadBytes(stream, 4, 0);
            return !string.IsNullOrWhiteSpace(data) && (data == "52-49-46-46");
        }

        private async Task<bool> IsMp3(Stream stream)
        {
            var data = await ReadBytes(stream, 3, 0);
            return !string.IsNullOrWhiteSpace(data) && (data == "49-44-33");
        }

        private async Task<string> ReadBytes(Stream stream, int byteCount, int offset)
        {
            var byteData = await _fileManager.ReadBytes(stream, byteCount: byteCount, offset: offset, dispose: false);
            if (byteData.Status != FileManagerStatus.Completed || byteData.Data == null) return null;

            return BitConverter.ToString(byteData.Data);
        }
    }
}