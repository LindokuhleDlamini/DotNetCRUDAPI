using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Application.Settings;
using Application.UseCases.External;
using Microsoft.Extensions.Configuration;

namespace Tests.IntegrationTests.External.UseCases
{
	public class TeamUseCasesTests
	{
		private readonly TeamUseCase _teamUseCase;
		public TeamUseCasesTests()
		{
			var services = new ServiceCollection();

			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();
			services.Configure<SportsApiSettings>(config.GetSection("SportsApiSettings"));

			services.AddHttpClient();
			services.AddScoped<TeamUseCase>();

			var serviceProvider = services.BuildServiceProvider();

			var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
			var httpClient = httpClientFactory.CreateClient();
			var apiSettings = serviceProvider.GetRequiredService<IOptions<SportsApiSettings>>();

			_teamUseCase = new TeamUseCase(httpClient, apiSettings);
		}

		[Fact]
		public async Task GetTeamByIdAsync_ShouldReturnTeam()
		{
			var teamId = "133604";

			var results = await _teamUseCase.GetTeamAsync(teamId);

			Assert.NotNull(results);
			
		}

        [Fact]
        public async Task GetAllTeamsAsync_ShouldReturnAListTeam()
        {
			var leagueName = "English Premier League";

            var results = await _teamUseCase.GetTeamsAsync(leagueName);

            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }
    }
}

