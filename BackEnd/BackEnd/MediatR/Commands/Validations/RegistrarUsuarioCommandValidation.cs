using BackEnd.Domain.IServices;
using FluentValidation;

namespace BackEnd.MediatR.Commands.Validations
{
    public class RegistrarUsuarioCommandValidation : AbstractValidator<RegistrarUsuarioCommandIdentity>
    {
        public RegistrarUsuarioCommandValidation(IUsuarioService usuarioService)
        {
            RuleFor(x => x.Usuario).NotNull();
            RuleFor(x => x.Usuario.Password).MinimumLength(8)
                .WithMessage("La contraseña tiene que tener minimo 8 caracteres");
        }
    }
}
