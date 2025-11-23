using CuacApp.Application.Services;
using CuacApp.Domain.Interfaces;
using CuacApp.Domain.Modelos;
using System.Diagnostics.Metrics;

namespace CuacApp.Application.Use_Case
{
    public class CambiarContrasena
    {
        private readonly IUsuarioRepository _usuario;
        private readonly ICodigoUsuario _codigoUsuario;
        private readonly IEnviarMensaje _enviarMensaje;
        private readonly Random _random = new Random();


        public CambiarContrasena(IUsuarioRepository usuario, ICodigoUsuario codigoUsuario, IEnviarMensaje enviarMensaje)
        {
            _usuario = usuario;
            _codigoUsuario = codigoUsuario;
            _enviarMensaje = enviarMensaje;
        }

        public async Task<(bool status, string mensaje)> EnviarCodigoRestablecer(string correo)
        {
            var usuarioExistente = await _usuario.BuscarUsuarioAsync(correo);
            if (usuarioExistente == null)
            {
                return (false, "No se encontró un usuario");
            }
            var codigo = _random.Next(100000, 1000000).ToString();
            var nuevoCode = new CodigoUsuario
            {
                Codigo = codigo,
                UsuariID = usuarioExistente.id
            };
            await _codigoUsuario.AgregarCodigoAsync(nuevoCode);
            await _codigoUsuario.GuardarCodigoAsync();
            await _enviarMensaje
                .GetEnviarMensaje(usuarioExistente.ConfirmacionCorreo,
                "Cambiar Contraseña",
                $"Este es su codigo para restablecer la contraseña: {codigo} ");

            return (true, $"Se ha enviado un codigo a {usuarioExistente.ConfirmacionCorreo} para restablecer la contraseña");

        }

        public async Task<(bool status, string mensaje)> ValidarAplicarCambios(string correo, string code, string nuevaContrasena)
        {
            var respuesta = await _codigoUsuario.ValidarCodigoAsync(correo, code);
            if (respuesta == null)
            {
                return (false, "Codigo invalido o no existe");
            }
            if (respuesta.CodigoUsado || respuesta.FechaExpiracin < DateTime.UtcNow)
            {
                return (false, "Codigo usado o expirado");
            }
            respuesta.CodigoUsado = true;
            await _codigoUsuario.GuardarCodigoAsync();

            var usuarioExistente = await _usuario.BuscarUsuarioAsync(correo);
            usuarioExistente.Contrasena = nuevaContrasena;
            await _usuario.GuardarUsuarioAsync();
            return (true, "Se ha actualizado tu contraseña");
        }

    }
}
