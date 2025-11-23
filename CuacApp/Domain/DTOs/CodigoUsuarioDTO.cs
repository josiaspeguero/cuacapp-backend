using CuacApp.Domain.Models;

namespace CuacApp.Domain.DTOs
{
    public class CodigoUsuarioDTO
    {
        public string Codigo { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaExpiracin { get; set; } = DateTime.UtcNow.AddMinutes(10);
        public bool CodigoUsado { get; set; } = false;
        public int UsuariID { get; set; }
    }
}
