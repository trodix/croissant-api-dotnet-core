using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CroissantApi.Models;
using AutoMapper;
using CroissantApi.Resources;
using CroissantApi.Extensions;
using CroissantApi.Domain.Services;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace CroissantApi.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RuleController : ControllerBase
    {
        private readonly IRuleService _ruleService;
        private readonly IMapper _mapper;

        public RuleController(IRuleService ruleService, IMapper mapper)
        {
            _ruleService = ruleService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all rules. 
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RuleResource>>> GetRule()
        {
            var rules = await _ruleService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Rule>, IEnumerable<RuleResource>>(rules);
            return Ok(resources);
        }

        /// <summary>
        /// Get one rule by id. 
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RuleResource>> GetRule(int id)
        {
            var rule = await _ruleService.FindAsync(id);

            if (rule == null)
            {
                return NotFound();
            }

            var resources = _mapper.Map<Rule, RuleResource>(rule);

            return Ok(resources);
        }

        /// <summary>
        /// update a rule. 
        /// </summary>
        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRule(int id, [FromBody] SaveRuleResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var rule = _mapper.Map<SaveRuleResource, Rule>(resource);
            var result = await _ruleService.UpdateAsync(id, rule);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var ruleResource = _mapper.Map<Rule, RuleResource>(result.Rule);
            return NoContent();
        }

        /// <summary>
        /// Create a rule. 
        /// </summary>
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostRule([FromBody] SaveRuleResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var rule = _mapper.Map<SaveRuleResource, Rule>(resource);
            var result = await _ruleService.SaveAsync(rule);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var ruleResource = _mapper.Map<Rule, RuleResource>(result.Rule);
            
            return CreatedAtAction(nameof(GetRule), new { id = ruleResource.Id }, ruleResource);
        }

        /// <summary>
        /// Delete a rule. 
        /// </summary>
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRule(int id)
        {
            var result = await _ruleService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
