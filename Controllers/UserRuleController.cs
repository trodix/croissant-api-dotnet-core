using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CroissantApi.Models;
using CroissantApi.Persistence.Context;
using AutoMapper;
using CroissantApi.Resources;
using CroissantApi.Domain.Services;
using CroissantApi.Extensions;

namespace CroissantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRuleController : ControllerBase
    {
        private readonly IUserRuleService _userRuleService;
        private readonly IMapper _mapper;

        public UserRuleController(IUserRuleService userRuleService, IMapper mapper)
        {
            _userRuleService = userRuleService;
            _mapper = mapper;
        }

        // GET: api/UserRule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRuleResource>>> GetUserRulesAll()
        {
            var userRules = await _userRuleService.ListAsync();
            var resources = _mapper.Map<IEnumerable<UserRule>, IEnumerable<UserRuleResource>>(userRules);
            return Ok(resources);
        }

        // GET: api/UserRule/users/5/rules/2
        [HttpGet("users/{userId}/rules/{ruleId}")]
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

        // GET: api/UserRule/users/5/rules
        [HttpGet("users/{userId}/rules")]
        public async Task<ActionResult<UserRuleResource>> GetUserRules(int userId)
        {
            var userRules = await _userRuleService.FindByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<UserRule>, IEnumerable<UserRuleResource>>(userRules);
            return Ok(resources);
        }

        // PUT: api/UserRule/users/5/rules/2
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("users/{userId}/rules/{ruleId}")]
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

        // POST: api/UserRule
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("users/{userId}")]
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
            return Ok(userRuleResource);
        }

        // DELETE: api/UserRule/users/5/rules/2
        [HttpDelete("users/{userId}/rules/{ruleId}")]
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
