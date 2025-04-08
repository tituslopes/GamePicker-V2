using GamePicker.Domain.Interfaces;
using GamePicker.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GamePicker.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient<ISteamService, SteamService>(client =>
                    {
                        client.BaseAddress = new Uri("http://steampowered.com/api/");
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                    });
                    services.AddTransient<IGenreService, AppGenreService>();
                    services.AddTransient<IGameRecommender, GameRecommenderService>();
                    services.AddTransient<GamePickerApp>();
                });

            using var host = builder.Build();

            try
            {
                var app = host.Services.GetRequiredService<GamePickerApp>();
                await app.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
            }
        }

        public class GamePickerApp
        {
            private readonly ISteamService _steamService;
            private readonly IGenreService _genreService;
            private readonly IGameRecommender _recommender;

            public GamePickerApp(ISteamService steamService, IGenreService genreService, IGameRecommender recommender)
            {
                _steamService = steamService;
                _genreService = genreService;
                _recommender = recommender;
            }

            public async Task RunAsync()
            {
                Console.WriteLine("Please inform your SteamAPI key: ");
                string? apiKey = Console.ReadLine();
                Console.WriteLine("Please inform your SteamID: ");
                string? steamId = Console.ReadLine();

                var targetGenre = await _genreService.GetTargetGenreFromRecentPlayed(apiKey, steamId);

                if (string.IsNullOrEmpty(targetGenre)) 
                {
                    Console.WriteLine("Couldn't define preferred genre from recently played games. Play something and comeback later");
                    return;
                }

                var ownedGames = await _steamService.GetOwnedGames(apiKey, steamId);

                var recommendation = await _recommender.RecommendRandomGameByGenre(ownedGames, targetGenre);

                if (recommendation != null)
                {
                    Console.WriteLine($"We recommend: {recommendation.Name}");
                    Console.WriteLine($"Genre: {targetGenre}");
                }
                else
                {
                    Console.WriteLine($"No {targetGenre} games found in your library.");
                }
            }
        }
    }
}
