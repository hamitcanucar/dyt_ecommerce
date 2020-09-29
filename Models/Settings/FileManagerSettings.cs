namespace dytsenayasar.Models.Settings
{
    public class FileManagerSettings
    {
        private int imageQuality;

        public string FilePath { get; set; }
        public string ImagePath { get; set; }
        public int MaxFileSizeInMB { get; set; }
        public int MaxImageSizeInMB { get; set; }
        public int ImagePixel { get; set; }
        public int ImageQuality
        {
            get { return imageQuality; }
            set
            {
                imageQuality = (value <= 0 || value > 100) ? 75 : value;
            }
        }
    }
}