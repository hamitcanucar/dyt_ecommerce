using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dytsenayasar.Models.NotificationService
{
    public enum NotificationDataType{UserContentsUpdated}

    public class NotificationData<T>
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationDataType Type { get; set; }
        public T Value { get; set; }
    }
}