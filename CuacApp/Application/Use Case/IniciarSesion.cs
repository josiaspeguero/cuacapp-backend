using CuacApp.Application.Services;
using CuacApp.Domain.Interfaces;
using CuacApp.Domain.Modelos;

namespace CuacApp.Application.Use_Case
{
    public class IniciarSesion
    {
        private readonly IUsuarioRepository _usuario;
        private readonly ICodigoUsuario _codigoUsuario;
        private readonly IEnviarMensaje _enviarMensaje;
        private readonly Random random = new Random();
        public IniciarSesion(IUsuarioRepository usuario, ICodigoUsuario codigoUsuario, IEnviarMensaje enviarMensaje)
        {
            _usuario = usuario;
            _codigoUsuario = codigoUsuario;
            _enviarMensaje = enviarMensaje;
        }
        public async Task<(bool status, string mensaje)> GetIniciarSesion(string correo, string contrasena)
        {
            var resultado = await _usuario.IniciarSesionAsync(correo, contrasena);
            if (resultado == null)
            {
                return (false, "No se encontró el usuario");
            }
            var codigo = random.Next(100000, 1000000).ToString();
            var nuevoCodigo = new CodigoUsuario
            {
                UsuariID = resultado.id,
                Codigo = codigo,
            };

            await _codigoUsuario.AgregarCodigoAsync(nuevoCodigo);
            await _codigoUsuario.GuardarCodigoAsync();
            await _enviarMensaje
                   .GetEnviarMensaje(resultado.ConfirmacionCorreo, "Iniciar sesion",
                   $"Este es tu codigo para iniciar sesion: {codigo}");

            return (true, $"Hemos enviado un codigo a {resultado.ConfirmacionCorreo} para iniciar sesion");
        }
        public async Task<(bool status, string mensaje)> ValidarSesion(string correo, string code)
        {
            var resultado = await _codigoUsuario.ValidarCodigoAsync(correo, code);
            if (resultado == null)
            {
                return (false, "Error al iniciar sesion. Codigo invalido");
            }
            if (resultado.FechaExpiracin < DateTime.UtcNow || resultado.CodigoUsado)
            {
                return (false, "Codigo expirado o utilizado");
            }

            return (true, "Inicio de sesion exitoso");
        }
    }
}
