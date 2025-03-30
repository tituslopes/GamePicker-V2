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
        }
    }
}