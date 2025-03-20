using Microsoft.AspNetCore.Mvc;
using Application.UseCases.External;
using Domain.External.Entities;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeagueController
	{
		private readonly LeagueUseCase _leagueUseCase;
		public LeagueController(LeagueUseCase leagueUseCase)
		{
			_leagueUseCase = leagueUseCase;
		}

        [HttpGet("id")]
        public async Task<ActionResult<League>> GetLeague(string id)
        {
            var league = await _leagueUseCase.GetLeagueAsync(id);

            if (league == null)
            {
                return new NotFoundResult();
            }
            return league;
        }

        [HttpGet]
        public async Task<ActionResult<List<League>>> GetAllLeagues()
        {
            var leagues = await _leagueUseCase.GetLeaguesAsync();

            return leagues;
        }

    }
}

