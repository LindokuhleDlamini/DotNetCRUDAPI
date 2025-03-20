using System;
using Application.UseCases;
using Application.UseCases.External;
using Domain.Entities;
using Domain.External.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController
	{
		private readonly TeamUseCase _teamUseCase;
		public TeamController(TeamUseCase teamUseCase)
		{
			_teamUseCase = teamUseCase;
		}

        [HttpGet("id")]
        public async Task<ActionResult<Team>> GetTeam(string id)
        {
            var team = await _teamUseCase.GetTeamAsync(id);

            if (team == null)
            {
                return new NotFoundResult();
            }
            return team;
        }

        [HttpGet("leagueName")]
        public async Task<ActionResult<List<Team>>> GetTeams(string leagueName)
        {
            var teams = await _teamUseCase.GetTeamsAsync(leagueName);

            return teams;
        }
    }
}

