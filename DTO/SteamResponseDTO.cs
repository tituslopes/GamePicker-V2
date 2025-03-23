using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GamePicker.DTO
{
    public class SteamResponseDTO
    {
        [JsonPropertyName("game_count")]
        public int GameCount { get; set; }
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
        [JsonPropertyName("games")]
        public List<GamesDTO> Games { get; set; }
    }
}
