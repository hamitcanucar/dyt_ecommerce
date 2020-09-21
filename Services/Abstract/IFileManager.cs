using System.IO;
using System.Threading.Tasks;

namespace dyt_ecommerce.Services.Abstract
{
    public enum FileManagerStatus{
        Completed,
        Failed,
        TooBigFile,
        FileNotFound,
        PathNotFound,
        PermissionDenied
    }

    public class FileManagerResult{
        public FileManagerStatus Status { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public Stream Stream { get; set; }
    }

    public interface IFileManager
    {
        Task<FileManagerResult> WriteFile(string fileName, Stream data);
        Task<FileManagerResult> WriteImage(string fileName, Stream data);
        Task<FileManagerResult> CreateThumbnailImage(string imgName);
        Task<FileManagerResult> ReadThumbnailImage(string fileName);
        FileManagerResult OpenThumbnailImageStream(string fileName);
        Task<FileManagerResult> ReadFile(string fileName);
        FileManagerResult OpenFileStream(string fileName);
        Task<FileManagerResult> ReadImage(string fileName);
        Task<FileManagerResult> ReadBytes(string fileName, int byteCount, int offset = 0, bool dispose = true);
        Task<FileManagerResult> ReadBytes(Stream data, int byteCount, int offset = 0, string fileName = null, bool dispose = true);
        FileManagerResult OpenImageStream(string fileName);
        Task<FileManagerResult> DeleteFile(string fileName);
        Task<FileManagerResult> DeleteImage(string fileName);
    }
}