using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CroissantApi.Models;

namespace CroissantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly CroissantContext _context;

        public RuleController(CroissantContext context)
        {
            _context = context;
        }

        // GET: api/Rule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rule>>> GetRule()
        {
            return await _context.Rule.ToListAsync();
        }

        // GET: api/Rule/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rule>> GetRule(int id)
        {
            var rule = await _context.Rule.FindAsync(id);

            if (rule == null)
            {
                return NotFound();
            }

            return rule;
        }

        // PUT: api/Rule/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRule(int id, Rule rule)
        {
            if (id != rule.Id)
            {
                return BadRequest();
            }

            _context.Entry(rule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rule
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rule>> PostRule(Rule rule)
        {
            _context.Rule.Add(rule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRule", new { id = rule.Id }, rule);
        }

        // DELETE: api/Rule/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rule>> DeleteRule(int id)
        {
            var rule = await _context.Rule.FindAsync(id);
            if (rule == null)
            {
                return NotFound();
            }

            _context.Rule.Remove(rule);
            await _context.SaveChangesAsync();

            return rule;
        }

        private bool RuleExists(int id)
        {
            return _context.Rule.Any(e => e.Id == id);
        }
    }
}
