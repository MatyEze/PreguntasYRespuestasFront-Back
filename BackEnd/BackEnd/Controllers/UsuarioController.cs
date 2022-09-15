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
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            
            try
            {
                var command = new RegistrarUsuarioCommandIdentity(usuario);
                var result = await _mediator.Send(command);
                return result != null ? Ok(new { message = $"usuario {result.NombreUsuario} registrado correctamente" })
                                        : (IActionResult)BadRequest(new { message = $"el usuario {usuario.NombreUsuario} ya esta registrado" });
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
                var command = new CambiarPasswordCommandIdentity(cambiarPassword, identity);
                var result = await _mediator.Send(command);
                return result;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
