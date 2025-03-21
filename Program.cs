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
                GetGames(apiKey, steamId, urlParameters);
            }
            private static async Task GetGames(string apiKey, string steamId, string urlParameters) 
            {
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
                        var apiResponse = await response.Content.ReadFromJsonAsync<SteamApiResponseDTO>();
                        if (apiResponse?.Response.Games != null)
                        {
                            foreach (var game in apiResponse.Response.Games)
                            {
                                Console.WriteLine($"Game: {game.Name}");
                                Console.WriteLine($"App ID: {game.AppId}");
                                Console.WriteLine($"Playtime (2 Weeks): {game.PlaytimeTwoWeeks / 60} hours");
                                Console.WriteLine($"Playtime: {game.PlaytimeForever / 60} hours");
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
        }
    }
}