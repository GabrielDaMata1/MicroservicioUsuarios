using FluentValidation;
using MicroservicioUsuarios.Application.DTOs;
namespace MicroservicioUsuarios.Application.Validator
{
    public class ActualizarPerfilDTOValidator : AbstractValidator<ActualizarPerfilDTO>
    {
        public ActualizarPerfilDTOValidator()
        {
            RuleFor(u => u.Nombre)
                .NotEmpty().WithMessage(" El nombre es obligatorio.");

            RuleFor(u => u.Correo)
                .NotEmpty().WithMessage(" El correo es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo es inválido.");

            RuleFor(u => u.Telefono)
                .Matches(@"^\d{11}$").WithMessage(" El teléfono debe contener 11 dígitos.");

            RuleFor(u => u.Direccion)
                .NotEmpty().WithMessage(" La dirección es obligatoria.");
        }
    }


}