using CuacApp.Domain.DTOs;
using CuacApp.Domain.Interfaces;
using CuacApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CuacApp.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task ActualizarUsuariAsync(UsuarioDTO usuario)
        {
            throw new NotImplementedException();
        }

        public async Task AgregarUsuarioAsync(Usuario usuario)
        {
            await _dbContext.AddAsync(usuario);
        }

        public async Task<bool> BuscarCedulaAsync(string documentoIdentidad)
        {
            var resultado = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.CedulaPasaporte == documentoIdentidad);
            if (resultado == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> BuscarCorreoAsync(string correo)
        {
            var resultado = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == correo);
            if (resultado == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Usuario> BuscarUsuarioAsync(string correo)
        {
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == correo);
            return usuario!;
        }

        public async Task<int> GuardarUsuarioAsync()
        {
            var resultado = await _dbContext.SaveChangesAsync();
            if (resultado <= 0)
            {
                return 0;
            }
            return 1;
        }

        public async Task<Usuario?> IniciarSesionAsync(string correo, string contrasena)
        {
            var resultado = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoElectronico == correo && u.Contrasena == contrasena);
            return resultado;
        }
    }
}
