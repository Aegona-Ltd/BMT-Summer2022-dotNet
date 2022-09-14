using Jwtauthentication6._0.Models;
using Jwtauthentication6._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Jwtauthentication6._0.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecuredController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetSecuredData()
        {
            var message = "This Secured Data is available only for Authenticated Users.";
            return Ok(message);
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PostSecuredData()
        {
            return Ok("This Secured Data is available only for Authenticated Users.");
        }

    }
}
