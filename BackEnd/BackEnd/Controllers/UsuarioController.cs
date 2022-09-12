using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.DTO;
using BackEnd.Services;
using BackEnd.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                var validarExistence = await _usuarioService.ValidarExistence(usuario);
                if (validarExistence)
                {
                    return BadRequest(new { message = "el usuario " + usuario.NombreUsuario + " ya esta registrado" });
                }

                usuario.Password = Encriptar.EncriptarString(usuario.Password);
                await _usuarioService.SaveUser(usuario);

                return Ok(new { message = "usuario registrado correctamente" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Route("CambiarPassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDTO cambiarPassword)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
                string passwordAnteriorEncriptada = Encriptar.EncriptarString(cambiarPassword.passwordAnterior);
                var usuario = await _usuarioService.ValidarPassword(idUsuario, passwordAnteriorEncriptada);
                if (usuario == null)
                {
                    return BadRequest(new { message = "Password incorrecta"});
                }
                else
                {
                    usuario.Password = Encriptar.EncriptarString(cambiarPassword.nuevaPassword);
                    await _usuarioService.UpdatePassword(usuario);
                    return Ok(new { message = "contraseña cambiada con exito" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
