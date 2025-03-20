using Microsoft.Extensions.DependencyInjection;
using Application.UseCases.External;
using Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Tests.IntegrationTests.External.UseCases
{
	public class LeagueUseCasesTests
	{
		private readonly LeagueUseCase _leagueUseCase;
		public LeagueUseCasesTests()
		{
            var services = new ServiceCollection();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            services.Configure<SportsApiSettings>(config.GetSection("SportsApiSettings"));

            services.AddHttpClient();
            services.AddScoped<LeagueUseCase>();

            var serviceProvider = services.BuildServiceProvider();

            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient();
            var apiSettings = serviceProvider.GetRequiredService<IOptions<SportsApiSettings>>();

            _leagueUseCase = new LeagueUseCase(httpClient, apiSettings);
        }

        [Fact]
        public async Task GetLeagueAsync_ShouldReturnALeague()
        {
            var leagueId = "4328";

            var results = await _leagueUseCase.GetLeagueAsync(leagueId);

            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetLeaguesAsync_ShouldReturnAListLeagues()
        {

            var results =await _leagueUseCase.GetLeaguesAsync();

            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }
    }
}

