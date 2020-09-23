namespace dytsenayasar.Models.Settings
{
    public class FileManagerSettings
    {
        private int thumbnailImageQuality;

        public string FilePath { get; set; }
        public string ImagePath { get; set; }
        public int MaxFileSizeInMB { get; set; }
        public int MaxImageSizeInMB { get; set; }
        public int ThumbnailImagePixel { get; set; }
        public int ThumbnailImageQuality
        {
            get { return thumbnailImageQuality; }
            set
            {
                thumbnailImageQuality = (value <= 0 || value > 100) ? 75 : value;
            }
        }
    }
}