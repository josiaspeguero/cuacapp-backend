using CuacApp.Domain.DTOs;
using CuacApp.Domain.Modelos;

namespace CuacApp.Domain.Interfaces
{
    public interface ICodigoUsuario
    {
        Task AgregarCodigoAsync(CodigoUsuario codigoUsuario);
        Task GuardarCodigoAsync();
        Task<CodigoUsuario?> ValidarCodigoAsync(string correo, string codigo);
    }
}
