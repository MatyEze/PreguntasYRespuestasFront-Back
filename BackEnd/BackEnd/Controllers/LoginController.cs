using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.MediatR.Queries;
using BackEnd.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Usuario usuario)
        {
            var query = new GetTokenLoginQuery(usuario);
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
