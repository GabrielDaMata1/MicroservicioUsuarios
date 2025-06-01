using FluentValidation;
using MicroservicioUsuarios.Application.DTOs;

namespace MicroservicioUsuarios.Application.Validator
{
    public class ContraseñaNuevaDTOValidator : AbstractValidator<ContraseñaNuevaDTO>
    {
        public ContraseñaNuevaDTOValidator()
        {
            RuleFor(u => u.Contraseña)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
        }
    }
}