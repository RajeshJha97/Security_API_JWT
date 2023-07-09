using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security_Dem2.Models;
using Security_Dem2.Utilities;
using System.Security.Claims;

namespace Security_Dem2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private utility util;
        public AuthController(IConfiguration config)
        {
            _config = config;
            util = new();
        }

        [HttpGet("GetConfig")]
        public ActionResult GetConfig() 
        {
            return Ok(_config.GetValue<string>("SecretKey"));
        }

        [HttpPost("SignIn")]
        public ActionResult Signin([FromBody] User user)
        {
            if (user == null) 
            {
                return BadRequest(new { 
                error="Please provide username and password"
                });
            }
            if (user.UserName == "admin" && user.Password == "password")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Email,"admin@admin.com"),
                    new Claim("admin","true")

                };
                var expiresAt = DateTime.Now.AddMinutes(10);
                string secretKey = _config.GetValue<string>("SecretKey");
                return Ok(new
                {
                    access_token =util.CreateToken(claims,expiresAt, secretKey),
                    expires_At= expiresAt,

                }); ;
            }
            ModelState.AddModelError("Unauthorised", "Incorrect username and password");
            return Unauthorized(ModelState);
        }
    }
}
