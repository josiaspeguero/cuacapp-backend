using AutoMapper;
using CuacApp.Domain.DTOs;
using CuacApp.Domain.Interfaces;

namespace CuacApp.Application.Use_Case
{
    public class ValidarCodigosUsuario
    {
        private readonly ICodigoUsuario _codigoUsuario;
        private readonly IUsuarioRepository _usuario;
        private readonly IMapper _mapper;

        public ValidarCodigosUsuario(ICodigoUsuario codigoUsuario, IUsuarioRepository usuario, IMapper mapper)
        {
            _codigoUsuario = codigoUsuario;
            _usuario = usuario;
            _mapper = mapper;
        }

        public async Task<(bool status, string mensaje)> ValidarCodigo(string correo, string code)
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

            var usuarioExistente = await _usuario.BuscarUsuarioAsync(correo);
            usuarioExistente.CuentaActivada = true;
            await _usuario.GuardarUsuarioAsync();
            respuesta.CodigoUsado = true;
            await _codigoUsuario.GuardarCodigoAsync();

            return (true, "Se ha activado tu cuenta");
        }
    }
}
