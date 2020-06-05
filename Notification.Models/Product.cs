using Newtonsoft.Json;

namespace Notification.Models
{
    public class Product
    {
        [JsonProperty("productId")]
        public string ProductId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("orderQuantity")]
        public decimal OrderQuantity { get; set; }
        [JsonProperty("uom")]
        public string UOM { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
