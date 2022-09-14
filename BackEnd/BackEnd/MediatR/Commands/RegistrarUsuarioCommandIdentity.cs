using BackEnd.Domain.Models;
using MediatR;

namespace BackEnd.MediatR.Commands
{
    public class RegistrarUsuarioCommandIdentity : IRequest<Usuario>
    {
        public Usuario Usuario { get; }

        public RegistrarUsuarioCommandIdentity(Usuario usuario)
        {
            Usuario = usuario;
        }
    }

}
