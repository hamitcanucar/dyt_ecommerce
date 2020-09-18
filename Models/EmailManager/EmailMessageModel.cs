using System.Collections.Generic;

namespace dyt_ecommerce.Models.EmailManager
{
    public class EmailMessageModel
    {
        public EmailMessageModel()
        {
            ToAdresses = new List<EmailAddressModel>();
            CCAdresses = new List<EmailAddressModel>();
        }

        public string Subject { get; set; }
        public string Content { get; set; }
        public ICollection<EmailAddressModel> ToAdresses { get; set; }
        public ICollection<EmailAddressModel> CCAdresses { get; set; }
    }
}