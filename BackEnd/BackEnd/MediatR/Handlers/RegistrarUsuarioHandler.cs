using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.MediatR.Commands;
using BackEnd.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.MediatR.Handlers
{
    public class RegistrarUsuarioHandler : IRequestHandler<RegistrarUsuarioCommandIdentity, Usuario>
    {
        private readonly IUsuarioService _usuarioService;

        public RegistrarUsuarioHandler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<Usuario> Handle(RegistrarUsuarioCommandIdentity request, CancellationToken cancellationToken)
        {
            var validarExistence = await _usuarioService.ValidarExistence(request.Usuario);
            if (validarExistence)
            {
                return null;
            }

            request.Usuario.Password = Encriptar.EncriptarString(request.Usuario.Password);
            await _usuarioService.SaveUser(request.Usuario);

            return request.Usuario;
        }
    }
}
