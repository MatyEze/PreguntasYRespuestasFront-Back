using BackEnd.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.MediatR.Queries
{
    public class GetTokenLoginQuery : IRequest<IActionResult>
    {
        public Usuario usuario { get; }

        public GetTokenLoginQuery(Usuario usuario)
        {
            this.usuario = usuario;
        }

    }
}
