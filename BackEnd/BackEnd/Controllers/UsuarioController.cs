using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.DTO;
using BackEnd.MediatR.Commands;
using BackEnd.Services;
using BackEnd.Utils;
using MediatR;
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
        private readonly IMediator _mediator;

        public UsuarioController(IUsuarioService usuarioService, IMediator mediator)
        {
            _usuarioService = usuarioService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            var command = new RegistrarUsuarioCommand(usuario);
            var result = await _mediator.Send(command);
            return result != null ? Ok(new { message = $"usuario {result.NombreUsuario} registrado correctamente" }) 
                                    : (IActionResult)BadRequest( new { message = $"el usuario {usuario.NombreUsuario} ya esta registrado"} );
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
