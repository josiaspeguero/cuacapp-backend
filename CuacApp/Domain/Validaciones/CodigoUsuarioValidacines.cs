using CuacApp.Domain.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CuacApp.Domain.Validaciones
{
    public class CodigoUsuarioValidacines : IEntityTypeConfiguration<CodigoUsuario>
    {
        public void Configure(EntityTypeBuilder<CodigoUsuario> builder)
        {
            builder.HasOne(cu => cu.Usuario)
                .WithMany(u => u.Codigo)
                .HasForeignKey(cu => cu.UsuariID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
