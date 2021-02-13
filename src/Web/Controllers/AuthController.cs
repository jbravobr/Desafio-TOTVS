using Desafio.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserServices _userService;

        public AuthController(IConfiguration configuration, IUserServices userServices)
        {
            _configuration = configuration;
            _userService = userServices;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/v1/auth")]
        public IActionResult Auth([FromBody] UserViewModel user)
        {
            try
            {
                var userExists = _userService.CheckUserExists(user.Email, user.Password);
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