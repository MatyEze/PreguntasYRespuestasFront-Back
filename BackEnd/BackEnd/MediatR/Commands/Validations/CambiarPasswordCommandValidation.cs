using FluentValidation;

namespace BackEnd.MediatR.Commands.Validations
{
    public class CambiarPasswordCommandValidation : AbstractValidator<CambiarPasswordCommandIdentity>
    {
        public CambiarPasswordCommandValidation()
        {
            RuleFor(x => x.CambiarPassword.nuevaPassword).MinimumLength(8)
                .WithMessage("MinimunLength_PASSWORD")
                .Must((x, nueavaPass) => x.CambiarPassword.passwordAnterior != nueavaPass)
                .WithMessage("la contraseñas deben ser diferentes");
        }
    }
}
