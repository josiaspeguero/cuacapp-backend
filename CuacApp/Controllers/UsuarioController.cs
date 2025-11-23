using CuacApp.Application.Use_Case;
using CuacApp.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuacApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly RegistrarUsuario _registrarUsuario;
        private readonly ValidarCodigosUsuario _validarCodigos;
        private readonly CambiarContrasena _cambiarContrasena;
        private readonly IniciarSesion _iniciarSesion;

        public UsuarioController(RegistrarUsuario registrarUsuario, ValidarCodigosUsuario validarCodigos,
            CambiarContrasena cambiarContrasena, IniciarSesion iniciarSesion)
        {
            _registrarUsuario = registrarUsuario;
            _validarCodigos = validarCodigos;
            _cambiarContrasena = cambiarContrasena;
            _iniciarSesion = iniciarSesion;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> RegistrarUsusario(UsuarioDTO usuarioDTO)
        {
            var result = await _registrarUsuario.CrearUsuario(usuarioDTO);
            if (!result.status)
            {
                return BadRequest(result.mensaje);
            }
            return Ok(result.mensaje);
        }

        [HttpPost("validar-cuenta")]
        public async Task<ActionResult> ActivarCuenta(string correo, string code)
        {
            var result = await _validarCodigos.ValidarCodigo(correo, code);
            if (!result.status)
            {
                return BadRequest(result.mensaje);
            }
            return Ok(result.mensaje);
        }

        [HttpPost("restablecer-contrasena")]
        public async Task<ActionResult> CambiarContrasena(string correo)
        {
            var result = await _cambiarContrasena.EnviarCodigoRestablecer(correo);
            return Ok(result.mensaje);
        }



        [HttpPost("restablecer-contrasena/nueva-contrasena")]
        public async Task<ActionResult> AplicarCambios(string correo, string code, string nuevaContrasena)
        {
            var result = await _cambiarContrasena.ValidarAplicarCambios(correo, code, nuevaContrasena);
            return Ok(result.mensaje);
        }

        [HttpPost("iniciar-sesion")]
        public async Task<ActionResult> IniciarSesion(string correo, string contrasena)
        {
            var resultado = await _iniciarSesion.GetIniciarSesion(correo, contrasena);
            return Ok(resultado.mensaje);
        }

        [HttpPost("iniciar-sesion/validar-inicio")]
        public async Task<ActionResult> ValidarInicioSesion(string correo, string code)
        {
            var resultado = await _iniciarSesion.ValidarSesion(correo, code);
            return Ok(resultado.mensaje);
        }
    }
}
