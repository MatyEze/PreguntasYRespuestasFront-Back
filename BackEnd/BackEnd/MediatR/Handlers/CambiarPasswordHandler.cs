using BackEnd.Domain.IServices;
using BackEnd.MediatR.Commands;
using BackEnd.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.MediatR.Handlers
{
    public class CambiarPasswordHandler : IRequestHandler<CambiarPasswordCommand, IActionResult>
    {
        private readonly IUsuarioService _usuarioService;

        public CambiarPasswordHandler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Handle(CambiarPasswordCommand request, CancellationToken cancellationToken)
        {
            int idUsuario = JwtConfigurator.GetTokenIdUsuario(request.Identity);
            string passwordAnteriorEncriptada = Encriptar.EncriptarString(request.CambiarPassword.passwordAnterior);
            var usuario = await _usuarioService.ValidarPassword(idUsuario, passwordAnteriorEncriptada);
            if (usuario == null)
            {
                return new BadRequestObjectResult(new { message = "Password incorrecta" });
            }
            else
            {
                usuario.Password = Encriptar.EncriptarString(request.CambiarPassword.nuevaPassword);
                await _usuarioService.UpdatePassword(usuario);
                return new OkObjectResult(new { message = "contraseña cambiada con exito" });
            }
        }
    }
}
