using Jwtauthentication6._0.Models;
using Jwtauthentication6._0.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Jwtauthentication6._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
                var result = await _userService.GetTokenAsync(model);
                SetRefreshTokenInCookie(result.RefreshToken);
                return Ok(new
                {
                    result,
                    cookie = result.RefreshToken,
                });
        }
        public static long GetTokenExpirationTime(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var ticks = long.Parse(tokenExp);
            return ticks;
        }

        public static bool CheckTokenIsValid(string token)
        {
            var tokenTicks = GetTokenExpirationTime(token);
            var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;

            var now = DateTime.Now.ToUniversalTime();

            var valid = tokenDate >= now;

            return valid;
        }
        [HttpGet("check/{token}")]
        public async Task<IActionResult> ValidateToken(string token)
        {
            if (CheckTokenIsValid(token))
            {
                var status = "200";
                return Ok(new
                {
                    status = status,
                });
            }
            else
            {
                return StatusCode(401);
            }
        }
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }
        
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10)
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _userService.RefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }
        //[Authorize]
        [HttpPost("tokens/{id}")]
        public IActionResult GetRefreshTokens(string id)
        {
            var user = _userService.GetById(id);
            return Ok(user.RefreshTokens);
        }
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });
            var response = _userService.RevokeToken(token);
            if (!response)
                return NotFound(new { message = "Token not found" });
            return Ok(new { message = "Token revoked" });
        }
    }
}
