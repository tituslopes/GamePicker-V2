using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GamePicker.Infrastructure.DTO
{
    public class OwnedGamesDTO
    {
        [JsonPropertyName("response")]
        public SteamResponseDTO Response { get; set; }
    }
}
