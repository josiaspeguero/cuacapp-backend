using CuacApp.Domain.DTOs;
using CuacApp.Domain.Models;

namespace CuacApp.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AgregarUsuarioAsync(Usuario usuario);
        Task<int> GuardarUsuarioAsync();
        Task<bool> BuscarCorreoAsync(string correo);
        Task<bool> BuscarCedulaAsync(string documentoIdentidad);
        Task<Usuario?> IniciarSesionAsync(string correo, string contrasena);
        Task<Usuario> BuscarUsuarioAsync(string correo);
        Task ActualizarUsuariAsync(UsuarioDTO usuario);
    }
}
