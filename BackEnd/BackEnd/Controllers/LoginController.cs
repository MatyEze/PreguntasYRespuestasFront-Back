using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _logingService;
        private readonly IConfiguration _configuration;
        public LoginController(ILoginService logingService, IConfiguration configuration)
        {
            _logingService = logingService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Usuario usuario)
        {


            try
            {
                usuario.Password = Encriptar.EncriptarString(usuario.Password);
                var user = await _logingService.ValidateUser(usuario);
                if (user == null)
                {
                    return BadRequest(new { message = "Usuario o contraseña incorrectos" });
                }
                else
                {
                    string tokenString = JwtConfigurator.GetToken(user, _configuration);
                    return Ok(new { token = tokenString });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
