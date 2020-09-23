using System.Collections.Generic;

namespace dytsenayasar.Models.NotificationService
{
    public class NotificationMessageModel<T>
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Action { get; set; }
        public NotificationData<T> Data { get; set; }
        public ICollection<string> ClientIds { get; set; }
    }
}