using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MicroservicioUsuarios.Application.Command;
using MicroservicioUsuarios.Application.DTOs;
using MicroservicioUsuarios.Application.Querys;
using MicroservicioUsuarios.Application.Validator;
using MicroservicioUsuarios.Application.Exceptions;
using MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB;
using MicroserviciosUsuarios.Domain.Entities;
namespace MicroservicioUsuarios.WebAPIUsuarios.Controllers
{

    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUsuarioMongoRepository _usuarioMongoRepository;

        public UsuarioController(IMediator mediator, IUsuarioMongoRepository usuarioMongoRepository)
        {
            _mediator = mediator;
            _usuarioMongoRepository = usuarioMongoRepository;

        }

        [HttpPost("registroUsuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioRegistroDTO usuarioDTO)
        {
            var validator = new UsuarioDTOValidator();
            var resultado = validator.Validate(usuarioDTO);

            if (!resultado.IsValid)
            {
                throw new UsuarioDTOException();
            }
            var usuarioId = await _mediator.Send(new RegistrarUsuarioCommand(usuarioDTO));
            return Ok(new { UsuarioId = usuarioId, Mensaje = "✅ Usuario registrado con éxito." });
        }

        [HttpPost("getUsuario")]
        public async Task<IActionResult> ConsultarCorreo([FromBody] ConsultarCorreoDTO correoDTO)
        {

            if (correoDTO == null)
                throw new AccesoNoAutorizadoException();
            var validator = new CorreoDTOValidator();
            var valCorreo = validator.Validate(correoDTO);

            if (!valCorreo.IsValid)
            {
                throw new CorreoInvalidoException();
            }
            var resultado = await _mediator.Send(new ConsultarCorreoQuery(correoDTO));
            return Ok(new { Existe = resultado });
        }

         [HttpPost("actualizarContrasena/{correo}")]
          public async Task<IActionResult> ConfirmarRecuperacion(string correo, [FromBody] ContraseñaNuevaDTO contraseñaDTO)
          {
              if (contraseñaDTO == null)
                  throw new AccesoNoAutorizadoException();
              var validator = new ContraseñaNuevaDTOValidator();  
              var valContraseña = validator.Validate(contraseñaDTO);

              if (!valContraseña.IsValid)
              {
                  throw new ContraseñaInvalidaException();
              }
              var resultado = await _mediator.Send(new ActualizarContraseñaCommand(contraseñaDTO, correo));
              return resultado? Ok("✅ Contraseña actualizada correctamente.") : BadRequest("❌ Token inválido o expirado.");
          }
      
        [HttpPut("actualizarPerfilUsuario/{correo}")]
        public async Task<IActionResult> ActualizarPerfil(string correo, [FromBody] ActualizarPerfilDTO actualizarPerfilDTO)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new CorreoInvalidoException();

            var validator = new ActualizarPerfilDTOValidator();
            var valActualizarPerfil = validator.Validate(actualizarPerfilDTO);

            if (!valActualizarPerfil.IsValid)
            {
                throw new DatosPerfillModificadoUsuarioException();
            }
            var resultado = await _mediator.Send(new ActualizarPerfilUsuarioCommand(actualizarPerfilDTO,correo));
            return resultado ? Ok("✅ Perfil actualizado correctamente.") : NotFound("❌ Usuario no encontrado.");

        }

        [HttpGet("historialActividades/{correo}")]
        public async Task<IActionResult> ObtenerHistorialActividad([FromRoute] string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new CorreoInvalidoException();

            var historial = await _mediator.Send(new ConsultarHistorialActividadQuery(correo));
            return Ok(historial);
        }

        [HttpGet("obtenerUsuarios")]
        public async Task<IActionResult> ObtenerTodosLosUsuarios()
        {
            var usuarios = await _mediator.Send(new ConsultarUsuariosQuery());
            return  Ok(usuarios);
        }

        [HttpPut("asignarRol/{correo}")]
        public async Task<IActionResult> AsignarRol([FromBody] AsignarRolDTO asignarRolDto, string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new CorreoInvalidoException();

            var validator = new AsignarRolDTOValidator();
            var valAsignarRoll = validator.Validate(asignarRolDto);

            if (!valAsignarRoll.IsValid)
            {
                throw new AsignarRolInvalidoException();
            }

            var resultado = await _mediator.Send(new AsignarRolCommand(correo, asignarRolDto));
            return resultado ? Ok("✅ Rol asignado correctamente.") : BadRequest("❌ Error al asignar rol.");
        }


    }
}