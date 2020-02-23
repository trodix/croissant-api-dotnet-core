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

namespace CroissantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all users. 
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserResource>>> GetUsers()
        {
            var users = await _userService.ListAsync();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return Ok(resources);
        }

        /// <summary>
        /// Get one user by id. 
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResource>> GetUser(int id)
        {
            var user = await _userService.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var resources = _mapper.Map<User, UserResource>(user);

            return Ok(resources);
        }

        /// <summary>
        /// update a user. 
        /// </summary>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUser(int id, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.UpdateAsync(id, user);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return NoContent();
        }

        /// <summary>
        /// Create a user. 
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostUser([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.SaveAsync(user);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userResource = _mapper.Map<User, UserResource>(result.User);

            return CreatedAtAction(nameof(GetUser), new { id = userResource.Id }, userResource);
        }

        /// <summary>
        /// Delete a user. 
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
