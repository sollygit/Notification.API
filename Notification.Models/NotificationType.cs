using Newtonsoft.Json;

namespace Notification.Models
{
    public class NotificationType
    {
        [JsonProperty("notificationTypeId")]
        public int NotificationTypeId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("isEnabled")]
        public bool IsEnabled { get; set; }
    }
}
