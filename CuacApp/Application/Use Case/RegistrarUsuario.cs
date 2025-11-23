using AutoMapper;
using CuacApp.Application.Services;
using CuacApp.Domain.DTOs;
using CuacApp.Domain.Interfaces;
using CuacApp.Domain.Modelos;
using CuacApp.Domain.Models;

namespace CuacApp.Application.Use_Case
{
    public class RegistrarUsuario
    {
        private readonly IUsuarioRepository _usuarios;
        private readonly IMapper _mapper;
        private readonly IEnviarMensaje _enviarMensaje;
        private readonly ICodigoUsuario _codigoUsuario;
        private readonly Random _random = new Random();

        public RegistrarUsuario(IUsuarioRepository usuarios, IMapper mapper,
            IEnviarMensaje enviarMensaje, ICodigoUsuario codigoUsuario)
        {
            _usuarios = usuarios;
            _mapper = mapper;
            _enviarMensaje = enviarMensaje;
            _codigoUsuario = codigoUsuario;
        }
        public async Task<(bool status, string mensaje)> CrearUsuario(UsuarioDTO usuario)
        {

            var nuevoUsuario = _mapper.Map<Usuario>(usuario);
            var correoExiste = await _usuarios.BuscarCorreoAsync(nuevoUsuario.CorreoElectronico);
            if (correoExiste)
            {
                return (false, "Ya existe un usuario con ese correo");
            }
            var cedulaEXistes = await _usuarios.BuscarCedulaAsync(nuevoUsuario.CedulaPasaporte);
            if (cedulaEXistes)
            {
                return (false, "Ya existe una cuenta asociada a esta cedula o pasaporte");

            }
            if (nuevoUsuario.CorreoElectronico == nuevoUsuario.ConfirmacionCorreo)
            {
                return (false, "El correo de confirmacion y el correo de la cuenta no pueden ser iguales");
            }

            await _usuarios.AgregarUsuarioAsync(nuevoUsuario);

            var usurioGuardado = await _usuarios.GuardarUsuarioAsync();

            if (usurioGuardado <= 0)
            {
                return (false, "Ha ocurrido un error creando el usuario");
            }
            var codigo = _random.Next(100000, 1000000).ToString();

            var usuarioID = await _usuarios.BuscarUsuarioAsync(nuevoUsuario.CorreoElectronico);

            var nuevoCodigo = new CodigoUsuario
            {
                Codigo = codigo,
                UsuariID = usuarioID.id,
            };

            await _codigoUsuario.AgregarCodigoAsync(nuevoCodigo);
            await _codigoUsuario.GuardarCodigoAsync();

            await _enviarMensaje.GetEnviarMensaje(nuevoUsuario.ConfirmacionCorreo, "Validar Perfil",
                $"Este es tu codigo de validacion {codigo}");


            return (true, $"Usuario creado correctamente. " +
                $"Hemos enviado un codigo que expira en 10 minutos a " +
                $"{nuevoUsuario.ConfirmacionCorreo} para validar tu perfil");
        }
    }
}
