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
using CroissantApi.Extensions;
using CroissantApi.Domain.Services;

namespace CroissantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IRuleService _ruleService;
        private readonly IMapper _mapper;

        public RuleController(IRuleService ruleService, IMapper mapper)
        {
            _ruleService = ruleService;
            _mapper = mapper;
        }

        // GET: api/Rule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleResource>>> GetRule()
        {
            var rules = await _ruleService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Rule>, IEnumerable<RuleResource>>(rules);
            return Ok(resources);
        }

        // GET: api/Rule/5
        [HttpGet("{id}")]
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

        // PUT: api/Rule/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
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
            return Ok(ruleResource);
        }

        // POST: api/Rule
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
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
            return Ok(ruleResource);
        }

        // DELETE: api/Rule/5
        [HttpDelete("{id}")]
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
