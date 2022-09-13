using BackEnd.Domain.Models;
using MediatR;

namespace BackEnd.MediatR.Queries
{
    public class ValidateExistenceUsuarioQuery : IRequest<Usuario>
    {
        public Usuario Usuario { get; }

        public ValidateExistenceUsuarioQuery(Usuario usuario)
        {
            Usuario = usuario;
        }
    }
        
}
