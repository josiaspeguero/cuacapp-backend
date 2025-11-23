using CuacApp.Domain.Modelos;

namespace CuacApp.Domain.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string Nombre { get; set; } = string.Empty; //
        public string Apellido { get; set; } = string.Empty; //
        public bool VisibilidadPerfil { get; set; } = true;
        public bool CuentaActivada { get; set; } = false;
        public DateTime FechaNacimiento { get; set; } //
        public string CedulaPasaporte { get; set; } = string.Empty; //
        public string CorreoElectronico { get; set; } = string.Empty; //
        public string ConfirmacionCorreo { get; set; } = string.Empty; //
        public string Contrasena { get; set; } = string.Empty;//
        public string Domicilio { get; set; } = string.Empty; 
        public string? Nacionalidad { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow; //automatica

        //relaciones
        public ICollection<CodigoUsuario> Codigo { get; set; } = new List<CodigoUsuario>();

    }
}
