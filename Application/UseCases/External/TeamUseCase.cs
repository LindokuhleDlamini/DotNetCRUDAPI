using System.Text.Json;
using Application.Settings;
using Domain.External.Entities;
using Microsoft.Extensions.Options;

namespace Application.UseCases.External
{
	public class TeamUseCase
	{
        private readonly HttpClient _httpClient;
        private readonly SportsApiSettings _sportsApiSettings;

        public TeamUseCase(HttpClient httpClient, IOptions<SportsApiSettings> sportsApiSettings)
		{
            _sportsApiSettings = sportsApiSettings.Value;
            _httpClient = httpClient;
		}

        public async Task<Team?> GetTeamAsync(string id)
        {
            var response = await _httpClient.GetStringAsync($"{_sportsApiSettings.BaseUrl}/lookupteam.php?id={id}");
            var teamResponse = JsonSerializer.Deserialize<TeamResponse>(response);

            if (teamResponse == null || teamResponse.Teams == null || teamResponse.Teams.Count == 0)
            {
                return null;
            }
            return teamResponse.Teams.FirstOrDefault();
        }

        public async Task<List<Team>> GetTeamsAsync(string leagueName)
        {
            var response = await _httpClient.GetStringAsync($"{_sportsApiSettings.BaseUrl}/search_all_teams.php?l={leagueName}");
            var teamResponse = JsonSerializer.Deserialize<TeamResponse>(response);

            if (teamResponse == null || teamResponse.Teams == null)
            {
                return new List<Team>();
            }
            return teamResponse.Teams;
        }

        private class TeamResponse
        {
            public required List<Team> Teams { get; set; }
        }
    }
}

