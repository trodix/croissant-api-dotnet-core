using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CroissantApi.Models;
using AutoMapper;
using CroissantApi.Resources;
using CroissantApi.Extensions;
using CroissantApi.Domain.Services;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace CroissantApi.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamController(ITeamService teamService, IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all teams.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TeamResource>>> GetTeams()
        {
            var teams = await _teamService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Team>, IEnumerable<TeamResource>>(teams);
            return Ok(resources);
        }

        /// <summary>
        /// Get one team by id. 
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamWithUsersResource>> GetTeam(int id)
        {
            var team = await _teamService.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            var resources = _mapper.Map<Team, TeamWithUsersResource>(team);

            return Ok(resources);
        }

        /// <summary>
        /// update a team. 
        /// </summary>
        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeamWithUsersResource>> PutTeam(int id, [FromBody] UpdateTeamResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var team = _mapper.Map<UpdateTeamResource, Team>(resource);
            var result = await _teamService.UpdateAsync(id, team);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var teamResource = _mapper.Map<Team, TeamWithUsersResource>(result.Team);
            return Ok(teamResource);
        }

        /// <summary>
        /// Create a team. 
        /// </summary>
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            var teamResource = _mapper.Map<Team, TeamWithUsersResource>(result.Team);
            return CreatedAtAction(nameof(GetTeam), new { id = teamResource.Id }, teamResource);

        }

        /// <summary>
        /// Delete a team. 
        /// </summary>
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
