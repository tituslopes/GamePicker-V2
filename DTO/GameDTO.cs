using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GamePicker.DTO
{
    public class GameDTO
    {
        [JsonPropertyName("appid")]
        public int AppId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("playtime_2weeks")]
        public int PlaytimeTwoWeeks { get; set; }
        [JsonPropertyName("playtime_forever")]
        public int PlaytimeForever { get; set; }
        [JsonPropertyName("img_icon_url")]
        public string? ImgIconUrl { get; set; }
        [JsonPropertyName("playtime_windows_forever")]
        public int PlaytimeWindowsForever { get; set; }
        [JsonPropertyName("playtime_mac_forever")]
        public int PlaytimeMacForever { get; set; }
        [JsonPropertyName("playtime_linux_forever")]
        public int PlaytimeLinuxForever { get; set; }
        [JsonPropertyName("playtime_deck_forever")]
        public int PlaytimeDeckForever { get; set; }
    }
}
