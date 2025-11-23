using CuacApp.Domain.Models;

namespace CuacApp.Domain.Modelos
{
    public class CodigoUsuario
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaExpiracin { get; set; } = DateTime.UtcNow.AddMinutes(10);
        public bool CodigoUsado { get; set; } = false;

        //propiedad de navegacion; relacion Usuario - Codigos
        public int UsuariID { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }
}
