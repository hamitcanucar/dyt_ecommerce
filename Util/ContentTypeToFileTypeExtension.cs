using dyt_ecommerce.Services.Abstract;
using dyt_ecommerce.DataAccess.Entities;

namespace dyt_ecommerce.Util
{
    public static class ContentTypeToFileTypeExtension
    {
        public static FileType ToFileType(this ContentType contentType)
        {
            switch(contentType)
            {
                case ContentType.Epub: return FileType.Epub;
                case ContentType.Mp4: return FileType.Mp4;
                case ContentType.Pdf: return FileType.Pdf;
                case ContentType.Png: return FileType.Png;
                case ContentType.Docx: return FileType.Docx;
                case ContentType.Doc: return FileType.Doc;
                default: return FileType.Any;
            }
        }
    }
}