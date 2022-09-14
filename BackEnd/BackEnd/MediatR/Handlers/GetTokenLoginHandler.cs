using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.MediatR.Queries;
using BackEnd.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.MediatR.Handlers
{
    public class GetTokenLoginHandler : IRequestHandler<GetTokenLoginQuery, IActionResult>
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;

        public GetTokenLoginHandler(IConfiguration configuration, ILoginService loginService)
        {
            _configuration = configuration;
            _loginService = loginService;
        }

        public async Task<IActionResult> Handle(GetTokenLoginQuery request, CancellationToken cancellationToken)
        {
            request.usuario.Password = Encriptar.EncriptarString(request.usuario.Password);
            var user = await _loginService.ValidateUser(request.usuario);
            if (user == null)
            {
                return new BadRequestObjectResult(new { message = "Usuario o contraseña incorrectos" });
            }
            else
            {
                string tokenString = JwtConfigurator.GetToken(user, _configuration);
                return new OkObjectResult(new { token = tokenString });
            }
        }
    }
}
