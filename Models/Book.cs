using Newtonsoft.Json;

namespace Models
{
    public class Book
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "year")]
        public string Publish_Year { get; set; }

        [JsonProperty(PropertyName = "price")]
        public string Base_Price { get; set; }

        [JsonProperty(PropertyName = "availability")]
        public string Book_Availability { get; set; }
    }
}
