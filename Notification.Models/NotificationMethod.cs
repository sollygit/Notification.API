using Newtonsoft.Json;

namespace Notification.Models
{
    public class NotificationMethod
    {
        [JsonProperty("notificationMethodId")]
        public int NotificationMethodId { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }
    }
}
