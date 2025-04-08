using Domain.GamePicker.Model;
using GamePicker.Domain.Interfaces;
using GamePicker.Domain.Model;
using GamePicker.Infrastructure.DTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GamePicker.Infrastructure.Services
{
    public class SteamService : ISteamService
    {
        private readonly HttpClient _client;

        public SteamService(HttpClient client)
        {
            _client = client;
        }
        public async Task<GameDetails?> GetAppDetails(int appId)
        {
            try
            {
                var response = await _client.GetAsync($"?appids={appId}");
                response.EnsureSuccessStatusCode();

                var dto = await response.Content.ReadFromJsonAsync<AppDetailsDTO>();

                if (dto?.Data == null || !dto.Success)
                {
                    return null;
                }

                return new GameDetails(
                    dto.Data.AppId,
                    dto.Data.Name ?? "Unknown Title",
                    dto.Data.Genres?
                    .Select(g => new Genre(g.Description ?? "Unknown Genre"))
                    .ToList() ?? new List<Genre>(),
                    dto.Data.Metacritic?.Score
                );
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<Game>> GetRecentlyPlayedGames(string apiKey, string steamId)
        {
            var games = new List<Game>();
            var urlParameters = $"?key={apiKey}&steamid={steamId}&format=json";

            try
            {
                var response = await _client.GetAsync(urlParameters);
                response.EnsureSuccessStatusCode();

                var dto = await response.Content.ReadFromJsonAsync<RecentlyPlayedGamesDTO>();
                games = dto?.Response.Games?
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

                var dto = await response.Content.ReadFromJsonAsync<OwnedGamesDTO>();
                games = dto?.Response.Games?
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
