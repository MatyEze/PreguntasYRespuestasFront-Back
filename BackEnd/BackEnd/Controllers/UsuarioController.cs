using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var validarExistence = await _usuarioService.ValidarExistence(usuario);
            if (validarExistence)
            {
                return BadRequest(new { message = "el usuario " + usuario.NombreUsuario + " ya esta registrado" });
            }

            try
            {
                await _usuarioService.SaveUser(usuario);

                return Ok(new { message = "usuario registrado correctamente" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
