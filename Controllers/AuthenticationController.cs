using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CroissantApi.Services;
using CroissantApi.Helpers;
using CroissantApi.Models;
using Microsoft.AspNetCore.Http;
using System;
using CroissantApi.Resources;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;

namespace CroissantApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticatedUserService _authenticationService;

        public AuthenticationController(IAuthenticatedUserService userService)
        {
            _authenticationService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Authenticate([FromBody] AuthenticateRequest model)
        {
            var response =  _authenticationService.Authenticate(model, ipAddress());

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult RefreshToken([FromBody] RefreshTokenRequest model)
        {
            // accept refreshToken from request body or cookie
            var refreshToken = model.RefreshToken ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest(new { message = "Refresh token is required" });

            var response =  _authenticationService.RefreshToken(refreshToken, ipAddress());

            if (response == null)
                return Unauthorized(new { message = "Invalid refresh token" });

            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [HttpPost("revoke-token")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response =  _authenticationService.RevokeToken(token, ipAddress());

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetAll()
        {
            var users =  _authenticationService.List();
            return Ok(ExtensionMethods.WithoutPasswords(users));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetById(int id)
        {
            var authenticatedUserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value);
            var authenticatedUserRole = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            if (authenticatedUserRole != Role.Admin)
            {
                if (authenticatedUserId != id) return Forbid();
            }

            var user =  _authenticationService.Find(id);
            if (user == null) return NotFound();

            return Ok(ExtensionMethods.WithoutPassword(user));
        }

        [HttpGet("{id}/refresh-tokens")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetRefreshTokens(int id)
        {
            var user =  _authenticationService.Find(id);
            if (user == null) return NotFound();

            return Ok(user.RefreshTokens);
        }

        // helper methods

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}