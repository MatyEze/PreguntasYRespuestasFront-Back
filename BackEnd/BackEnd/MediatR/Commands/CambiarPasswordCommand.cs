﻿using BackEnd.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.MediatR.Commands
{
    public class CambiarPasswordCommand : IRequest<IActionResult>
    {
        public CambiarPasswordDTO CambiarPassword { get; }
        public ClaimsIdentity Identity { get; set; }

        public CambiarPasswordCommand(CambiarPasswordDTO cambiarPassword, ClaimsIdentity identity)
        {
            this.CambiarPassword = cambiarPassword;
            this.Identity = identity;
        }
    }
}
