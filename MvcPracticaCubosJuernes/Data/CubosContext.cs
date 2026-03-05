using Microsoft.EntityFrameworkCore;
using MvcPracticaCubosJuernes.Models;

namespace MvcPracticaCubosJuernes.Data
{
    public class CubosContext : DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options)
            : base(options) { }

        public DbSet<Cubo> Cubos { get; set; }
        public DbSet<Compra> Compras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cubo>(entity =>
            {
                entity.ToTable("CUBOS");
                entity.HasKey(e => e.IdCubo);
                entity.Property(e => e.IdCubo).HasColumnName("id_cubo");
                entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(500);
                entity.Property(e => e.Modelo).HasColumnName("modelo").HasMaxLength(500);
                entity.Property(e => e.Marca).HasColumnName("marca").HasMaxLength(500);
                entity.Property(e => e.Imagen).HasColumnName("imagen").HasMaxLength(500);
                entity.Property(e => e.Precio).HasColumnName("precio");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.ToTable("COMPRA");
                entity.HasKey(e => new { e.IdCompra, e.IdCubo });
                entity.Property(e => e.IdCompra).HasColumnName("id_compra");
                entity.Property(e => e.IdCubo).HasColumnName("id_cubo");
                entity.Property(e => e.Cantidad).HasColumnName("cantidad");
                entity.Property(e => e.Precio).HasColumnName("precio");
                entity.Property(e => e.FechaPedido).HasColumnName("fechapedido");
            });
        }
    }
}
