using GamePicker.DTO;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace GamePicker
{
    internal class Program
    {
        public class Class1
        {
            private const string URL = "https://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/";

            static HttpClient client = new HttpClient();
            static void Main(string[] args)
            {
                Console.WriteLine("Please inform the API Key: ");
                string? apiKey = Console.ReadLine();
                Console.WriteLine("Please inform the Steam ID: ");
                string? steamId = Console.ReadLine();
                string urlParameters = $"?key={apiKey}&steamid={steamId}&format=json";
                GetRecentlyPlayedGames(apiKey, steamId, urlParameters);
            }
            private static async Task GetRecentlyPlayedGames(string apiKey, string steamId, string urlParameters)
            {
                var gamesDict = new Dictionary<int, string>();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    string fullUrl = URL + urlParameters;
                    Console.WriteLine($"Request URL: {fullUrl}");

                    HttpResponseMessage response = client.GetAsync(fullUrl).Result;
                    Console.WriteLine($"HTTP Status Code: {response.StatusCode} ({response.ReasonPhrase})");
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadFromJsonAsync<RecentlyPlayedGamesDTO>();
                        if (apiResponse?.Response.Games != null)
                        {
                            foreach (var game in apiResponse.Response.Games)
                            {
                                gamesDict.Add(game.AppId, game.Name);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP Request Error: {ex.Message}");
                }
            }

            private static async Task GetOwnedGames(string apiKey, string steamId, string urlParameters)
            {
                var ownedGamesDict = new Dictionary<int, string>();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    string fullUrl = URL + urlParameters;
                    Console.WriteLine($"Request URL: {fullUrl}");

                    HttpResponseMessage response = client.GetAsync(fullUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadFromJsonAsync<OwnedGamesDTO>();
                        if (apiResponse?.Response.Games != null)
                        {
                            foreach (var game in apiResponse.Response.Games)
                            {
                                ownedGamesDict.Add(game.AppId, game.Name);
                            }
                        }
                    }
                }

                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP Request Error: {ex.Message}");
                }
            }

        }
    }
}