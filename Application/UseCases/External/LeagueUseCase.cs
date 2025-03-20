using System.Text.Json;
using Application.Settings;
using Domain.External.Entities;
using Microsoft.Extensions.Options;

namespace Application.UseCases.External
{
	public class LeagueUseCase
	{
		private readonly HttpClient _httpClient;
        private readonly SportsApiSettings _sportsApiSettings;

        public LeagueUseCase(HttpClient httpClient, IOptions<SportsApiSettings> sportsApiSettings)
		{
            _sportsApiSettings = sportsApiSettings.Value;
			_httpClient = httpClient;
		}

		public async Task<League?> GetLeagueAsync(string id)
		{
            var response = await _httpClient.GetStringAsync($"{_sportsApiSettings.BaseUrl}/lookupleague.php?id={id}");
            var leagueResponse = JsonSerializer.Deserialize<LeagueResponse>(response);

            if (leagueResponse == null || leagueResponse.Leagues == null || leagueResponse.Leagues.Count == 0)
            {
                return null;
            }
            return leagueResponse.Leagues.FirstOrDefault();
        }

		public async Task<List<League>> GetLeaguesAsync()
		{
            var response = await _httpClient.GetStringAsync($"{_sportsApiSettings.BaseUrl}/all_leagues.php");
            var leagueResponse = JsonSerializer.Deserialize<LeagueResponse>(response);

            if (leagueResponse == null || leagueResponse.Leagues == null)
            {
                return new List<League>();
            }
            return leagueResponse.Leagues;
        }

        private class LeagueResponse
        {
            public required List<League> Leagues { get; set; }
        }
    }
}

