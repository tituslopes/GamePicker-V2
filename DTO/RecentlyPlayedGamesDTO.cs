﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GamePicker.DTO
{
    public class RecentlyPlayedGamesDTO
    {
        [JsonPropertyName("response")]
        public SteamResponseDTO Response { get; set; }
    }
}
