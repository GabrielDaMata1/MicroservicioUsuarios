using FluentValidation;
using MicroservicioUsuarios.Application.DTOs;

namespace MicroservicioUsuarios.Application.Validator
{
    public class AsignarRolDTOValidator: AbstractValidator<AsignarRolDTO>
    {
        public AsignarRolDTOValidator() {

            RuleFor(u => u.nombre_rol)
                .NotEmpty().WithMessage(" El nombre del rol es obligatorio.");
        }

    }
}
