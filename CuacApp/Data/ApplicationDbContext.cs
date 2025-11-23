using CuacApp.Domain.Modelos;
using CuacApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CuacApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<CodigoUsuario> CodigoUsuarios => Set<CodigoUsuario>();
    }
}
