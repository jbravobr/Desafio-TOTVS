using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/v1/auth")]
        public async Task<IActionResult> Auth([FromBody] UserViewModel user)
        {
            try
            {
                var userExists = null;
                if (userExists == null || userExists.Password != user.Password)
                    return BadRequest(new { Message = "Email e/ou senha está(ão) inválido(s)." });

                var key = _configuration.GetValue<string>("AuthenticationSettings:Key");
                var token = JwtAuth.GenerateToken(Encoding.ASCII.GetBytes(key));

                return Ok(new
                {
                    Token = token,
                    Usuario = userExists
                });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente." });
            }
        }
    }
}