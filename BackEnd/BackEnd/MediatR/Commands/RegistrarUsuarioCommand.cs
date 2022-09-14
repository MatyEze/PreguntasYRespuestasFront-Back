using BackEnd.Domain.Models;
using MediatR;

namespace BackEnd.MediatR.Commands
{
    public class RegistrarUsuarioCommand : IRequest<Usuario>
    {
        public Usuario Usuario { get; }

        public RegistrarUsuarioCommand(Usuario usuario)
        {
            Usuario = usuario;
        }
    }

}
