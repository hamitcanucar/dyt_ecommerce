using System;
using System.IO;
using System.Threading.Tasks;

namespace dyt_ecommerce.Services.Abstract
{
    [Flags]
    public enum FileType : ulong
    {
        Any = 0,
        Pdf = 1 << 0,
        Doc = 1 << 1,
        Docx = 1 << 2,
        Epub = 1 << 3,
        Png = 1 << 4,
        Bmp = 1 << 5,
        Gif = 1 << 6,
        Jpg = 1 << 7,
        Mp4 = 1 << 8,
        Flv = 1 << 9,
        _3gp = 1 << 10,
        Avi = 1 << 11,
        Mp3 = 1 << 12,
        Zip = 1 << 13,


        Image = Png | Bmp | Gif | Jpg,
        Video = Flv | _3gp | Avi,
        Document = Doc | Docx
    }

    public interface IFileTypeChecker
    {
        Task<bool> IsFileTypeCorrect(Stream stream, FileType fileType); 
    }
}