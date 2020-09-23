using dytsenayasar.Services.Abstract;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Util
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