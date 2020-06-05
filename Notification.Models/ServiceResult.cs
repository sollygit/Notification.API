using Newtonsoft.Json;

namespace Notification.Models
{
    public class ServiceResult
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
