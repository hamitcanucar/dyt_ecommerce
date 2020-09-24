using dytsenayasar.Services.Abstract;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Util
{
    public static class ContentTypeToFileTypeExtension
    {
        public static FileType ToFileType(this ContentTypes contentType)
        {
            switch(contentType)
            {
                case ContentTypes.Epub: return FileType.Epub;
                case ContentTypes.Mp4: return FileType.Mp4;
                case ContentTypes.Pdf: return FileType.Pdf;
                case ContentTypes.Png: return FileType.Png;
                case ContentTypes.Docx: return FileType.Docx;
                case ContentTypes.Doc: return FileType.Doc;
                default: return FileType.Any;
            }
        }
    }
}