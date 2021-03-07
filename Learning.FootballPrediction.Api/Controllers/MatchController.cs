using System;
using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Contracts;
using Learning.FootballPrediction.Api.Models;
using Learning.FootballPrediction.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Learning.FootballPrediction.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchRepository _matchRepo;
        private readonly IClubRepository _clubRepo;
        private readonly IPlayerRepository _playerRepo;

        public MatchController(IMatchRepository match, IClubRepository clubs, IPlayerRepository playerRepo)
        {
            this._matchRepo = match;
            this._clubRepo = clubs;
            this._playerRepo = playerRepo;
        }

        [HttpPost("save")]
        public async Task<Match> CreateMatch([FromBody]MatchRequest match)
        {                    
            MatchCreator creator = new MatchCreator(this._clubRepo, this._matchRepo, this._playerRepo);        
            var result = await creator.RunWorkflow(match);
        
            return result;            
        }
    }
}