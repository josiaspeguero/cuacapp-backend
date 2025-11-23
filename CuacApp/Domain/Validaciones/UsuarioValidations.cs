using CuacApp.Domain.Models;
using FluentValidation;

namespace CuacApp.Domain.Validations
{
    public class UsuarioValidations : AbstractValidator<Usuario>
    {
        public UsuarioValidations()
        {
            RuleFor(u => u.Nombre).NotEmpty().WithMessage("EL nombre no puede estar en blanco");
            RuleFor(u => u.Apellido).NotEmpty().WithMessage("EL apellido no puede estar en blanco");
            RuleFor(u => u.CorreoElectronico).NotEmpty().WithMessage("EL correo no puede estar en blanco");
            RuleFor(u => u.ConfirmacionCorreo).NotEmpty().WithMessage("EL correo de verificacion no puede estar en blanco");
        }
    }
}
