namespace dyt_ecommerce.Models.Settings
{
    public class EmailManagerSettings
    {
        public string SenderName { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}