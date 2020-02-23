using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CroissantApi.Models;
using AutoMapper;
using CroissantApi.Resources;
using CroissantApi.Extensions;
using CroissantApi.Domain.Services;

namespace CroissantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamController(ITeamService teamService, IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }

        // GET: api/Team
        /// <summary>
        /// Get all teams.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamResource>>> GetTeams()
        {
            var teams = await _teamService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Team>, IEnumerable<TeamResource>>(teams);
            return Ok(resources);
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamResource>> GetTeam(int id)
        {
            var team = await _teamService.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            var resources = _mapper.Map<Team, TeamResource>(team);

            return Ok(resources);
        }

        // PUT: api/Team/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, [FromBody] SaveTeamResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var team = _mapper.Map<SaveTeamResource, Team>(resource);
            var result = await _teamService.UpdateAsync(id, team);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var teamResource = _mapper.Map<Team, TeamResource>(result.Team);
            return Ok(teamResource);
        }

        // POST: api/Team
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<IActionResult> PostTeam([FromBody] SaveTeamResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var team = _mapper.Map<SaveTeamResource, Team>(resource);
            var result = await _teamService.SaveAsync(team);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var teamResource = _mapper.Map<Team, TeamResource>(result.Team);
            return Ok(teamResource);
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var result = await _teamService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
