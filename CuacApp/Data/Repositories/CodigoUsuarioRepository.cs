using CuacApp.Domain.DTOs;
using CuacApp.Domain.Interfaces;
using CuacApp.Domain.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CuacApp.Data.Repositories
{
    public class CodigoUsuarioRepository : ICodigoUsuario
    {
        private readonly ApplicationDbContext _dbContext;

        public CodigoUsuarioRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AgregarCodigoAsync(CodigoUsuario codigoUsuario)
        {
            await _dbContext.AddAsync(codigoUsuario);
        }

        public async Task GuardarCodigoAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CodigoUsuario?> ValidarCodigoAsync(string correo, string codigo)
        {
            var resultado = await _dbContext.CodigoUsuarios
                .FirstOrDefaultAsync(cu=>cu.Codigo == codigo && cu.Usuario.CorreoElectronico == correo);

            return resultado;
        }

       
    }
}
