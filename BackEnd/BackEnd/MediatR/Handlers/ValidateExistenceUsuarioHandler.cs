using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.MediatR.Queries;
using BackEnd.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.MediatR.Handlers
{
    public class ValidateExistenceUsuarioHandler : IRequestHandler<ValidateExistenceUsuarioQuery, Usuario>
    {
        private readonly IUsuarioService _usuarioService;

        public ValidateExistenceUsuarioHandler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<Usuario> Handle(ValidateExistenceUsuarioQuery request, CancellationToken cancellationToken)
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
