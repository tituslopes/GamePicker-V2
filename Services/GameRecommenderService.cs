using Domain.GamePicker.Model;
using GamePicker.Domain.Interfaces;
using GamePicker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePicker.Infrastructure.Services
{
    public class GameRecommenderService : IGameRecommender
    {
        private readonly ISteamService _steamService;

        public GameRecommenderService(ISteamService steamService)
        {
            _steamService = steamService;
        }

        public async Task<GameDetails?> RecommendRandomGameByGenre(
            IEnumerable<Game> games,
            string targetGenre
        )
        {
            var validGames = new List<GameDetails>();
            var random = new Random();

            foreach (var game in games)
            {
                var details = await _steamService.GetAppDetails(game.AppId);
                if (details?.Genres.Any(g =>
                    g.Description.Equals(targetGenre, StringComparison.OrdinalIgnoreCase)) == true)
                {
                    validGames.Add(details);
                }
            }

            return validGames.Count > 0
                ? validGames[random.Next(validGames.Count)]
                : null;
        }
    }
}
