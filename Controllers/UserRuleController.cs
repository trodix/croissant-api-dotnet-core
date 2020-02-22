using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CroissantApi.Models;
using CroissantApi.Persistence.Context;

namespace CroissantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRuleController : ControllerBase
    {
        private readonly CroissantContext _context;

        public UserRuleController(CroissantContext context)
        {
            _context = context;
        }

        // GET: api/UserRule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRule>>> GetUserRules()
        {
            return await _context.UserRules.ToListAsync();
        }

        // GET: api/UserRule/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRule>> GetUserRule(int id)
        {
            var userRule = await _context.UserRules.FindAsync(id);

            if (userRule == null)
            {
                return NotFound();
            }

            return userRule;
        }

        // PUT: api/UserRule/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRule(int id, UserRule userRule)
        {
            if (id != userRule.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userRule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRuleExists(id))
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

        // POST: api/UserRule
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserRule>> PostUserRule(UserRule userRule)
        {
            _context.UserRules.Add(userRule);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserRuleExists(userRule.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserRule", new { id = userRule.UserId }, userRule);
        }

        // DELETE: api/UserRule/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserRule>> DeleteUserRule(int id)
        {
            var userRule = await _context.UserRules.FindAsync(id);
            if (userRule == null)
            {
                return NotFound();
            }

            _context.UserRules.Remove(userRule);
            await _context.SaveChangesAsync();

            return userRule;
        }

        private bool UserRuleExists(int id)
        {
            return _context.UserRules.Any(e => e.UserId == id);
        }
    }
}
