using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GamePicker.Infrastructure.DTO
{

    public class AppDetailsDTO
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public AppDetailsDataDTO? Data { get; set; }
    }
    public class AppDetailsDataDTO
    {
        [JsonPropertyName("appid")]
        public int AppId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("genres")]
        public List<GenreDTO>? Genres { get; set; }
        [JsonPropertyName("metacritic")]
        public MetacriticInfoDTO? Metacritic { get; set; }
    }

    public class GenreDTO
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class MetacriticInfoDTO
    {
        [JsonPropertyName("score")]
        public int Score { get; set; }
    }
}
