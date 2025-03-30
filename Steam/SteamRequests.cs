using Domain.GamePicker.Model;
using GamePicker.Domain.Interfaces;
using GamePicker.DTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GamePicker.Infraestructure
{
    class SteamRequests(HttpClient client) : ISteamRequests
    {
        private readonly HttpClient _client = client;

        public async Task<IEnumerable<Game>> GetRecentlyPlayedGame(string apiKey, string steamId)
        {
            var games = new List<Game>();
            var urlParameters = $"?key={apiKey}&steamid={steamId}&format=json";

            try
            {
                var response = await _client.GetAsync(urlParameters);
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadFromJsonAsync<RecentlyPlayedGamesDTO>();
                games = apiResponse?.Response.Games?
                    .Select(x => new Game(x.AppId, x.Name))
                    .ToList() ?? new List<Game>();

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
            }

            return games;
        }

        public async Task<IEnumerable<Game>> GetOwnedGames(string apiKey, string steamId)
        {
            var games = new List<Game>();
            var urlParameters = $"?key={apiKey}&steamid={steamId}&format=json";

            try
            {
                var response = await _client.GetAsync(urlParameters);
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadFromJsonAsync<RecentlyPlayedGamesDTO>();
                games = apiResponse?.Response.Games?
                    .Select(x => new Game(x.AppId, x.Name))
                    .ToList() ?? new List<Game>();

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
            }

            return games;
        }
    }
}
