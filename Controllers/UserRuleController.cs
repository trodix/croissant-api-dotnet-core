using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CroissantApi.Models;
using AutoMapper;
using CroissantApi.Resources;
using CroissantApi.Domain.Services;
using CroissantApi.Extensions;
using System.Net.Mime;

namespace CroissantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserRuleController : ControllerBase
    {
        private readonly IUserRuleService _userRuleService;
        private readonly IMapper _mapper;

        public UserRuleController(IUserRuleService userRuleService, IMapper mapper)
        {
            _userRuleService = userRuleService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all user rules. 
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserRuleResource>>> GetUserRulesAll()
        {
            var userRules = await _userRuleService.ListAsync();
            var resources = _mapper.Map<IEnumerable<UserRule>, IEnumerable<UserRuleResource>>(userRules);
            return Ok(resources);
        }

        /// <summary>
        /// Get one specific rule for a specific user. 
        /// </summary>
        [HttpGet("users/{userId}/rules/{ruleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserRuleResource>> GetUserRule(int userId, int ruleId)
        {
            var userRule = await _userRuleService.FindAsync(userId, ruleId);

            if (userRule == null)
            {
                return NotFound();
            }

            var resources = _mapper.Map<UserRule, UserRuleResource>(userRule);

            return Ok(resources);
        }

        /// <summary>
        /// Get all rules for a specific user. 
        /// </summary>
        [HttpGet("users/{userId}/rules")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserRuleResource>> GetUserRules(int userId)
        {
            var userRules = await _userRuleService.FindByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<UserRule>, IEnumerable<UserRuleResource>>(userRules);
            return Ok(resources);
        }

        /// <summary>
        /// update a rule for a specific user. 
        /// </summary>
        [HttpPut("users/{userId}/rules/{ruleId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUserRule(int userId, int ruleId, [FromBody] UpdateUserRuleResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var userRule = _mapper.Map<UpdateUserRuleResource, UserRule>(resource);
            var result = await _userRuleService.UpdateAsync(userId, ruleId, userRule);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userRuleResource = _mapper.Map<UserRule, UserRuleResource>(result.UserRule);
            return Ok(userRuleResource);
        }

        /// <summary>
        /// Create a rule for a specific user. 
        /// </summary>
        [HttpPost("users/{userId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostUserRule(int userId, [FromBody] SaveUserRuleResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var userRule = _mapper.Map<SaveUserRuleResource, UserRule>(resource);
            var result = await _userRuleService.SaveAsync(userId, userRule);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userRuleResource = _mapper.Map<UserRule, UserRuleResource>(result.UserRule);
            
            return CreatedAtAction(
                nameof(GetUserRule), 
                new { userId = userRuleResource.User.Id, ruleId = userRuleResource.Rule.Id }, 
                userRuleResource);
        }

        /// <summary>
        /// Delete a rule for a specific user. 
        /// </summary>
        [HttpDelete("users/{userId}/rules/{ruleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserRule>> DeleteUserRule(int userId, int ruleId)
        {
            var result = await _userRuleService.DeleteByUserIdAsync(userId, ruleId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
