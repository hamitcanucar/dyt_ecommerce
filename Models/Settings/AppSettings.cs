namespace dyt_ecommerce.Models.Settings
{
    public class AppSettings
    {
        public string Issuer { get; set; }
        public string AuthSecret { get; set; }
        public string FileRequestSecret { get; set; }
        public int JwtValidityInMinutes { get; set; }
        public string Address { get; set; }
    }
}