using GamePicker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePicker.Infrastructure.Services
{
    public class AppGenreService : IGenreService
    {
        private readonly ISteamService _steamService;

        public AppGenreService(ISteamService steamService)
        {
            _steamService = steamService;
        }

        public async Task<string?> GetTargetGenreFromRecentPlayed(string apiKey, string steamId)
        {
            var recentGames = await _steamService.GetRecentlyPlayedGames(apiKey, steamId);

            var firstGame = recentGames.FirstOrDefault();
            if (firstGame == null)
            {
                return null;
            }

            var details = await _steamService.GetAppDetails(firstGame.AppId);

            return details?.Genres.FirstOrDefault()?.Description;
        }
    }
}
