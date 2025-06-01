using FluentValidation;
using MicroservicioUsuarios.Application.DTOs;

namespace MicroservicioUsuarios.Application.Validator
{
    public class CorreoDTOValidator : AbstractValidator<ConsultarCorreoDTO>
{
    public CorreoDTOValidator()
    {
            RuleFor(u => u.Correo)
                .NotEmpty().WithMessage(" El correo es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo es inválido.");
    }
}
}