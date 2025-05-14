using Microsoft.EntityFrameworkCore;
using WarframeRivensAPI.Models;

namespace WarframeRivensAPI.Data
{
    public partial class WarRivenContext : DbContext
    {
        public WarRivenContext(DbContextOptions<WarRivenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Riven> Rivens { get; set; }
        public virtual DbSet<Oferta> Ofertas { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<Favorito> Favoritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Riven>()
                .HasOne(r => r.Propietario)
                .WithMany()
                .HasForeignKey(r => r.IdPropietario);

            modelBuilder.Entity<Riven>()
                .Property(r => r.Valor1)
                .HasPrecision(10, 3);
            modelBuilder.Entity<Riven>()
                .Property(r => r.Valor2)
                .HasPrecision(10, 3);
            modelBuilder.Entity<Riven>()
                .Property(r => r.Valor3)
                .HasPrecision(10, 3);
            modelBuilder.Entity<Riven>()
                .Property(r => r.DValor)
                .HasPrecision(10, 3);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Comprador)
                .WithMany()
                .HasForeignKey(v => v.IdComprador)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Vendedor)
                .WithMany()
                .HasForeignKey(v => v.IdVendedor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venta>()
                .Property(v => v.PrecioVenta)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Oferta>()
                .HasOne(o => o.Comprador)
                .WithMany()
                .HasForeignKey(o => o.IdComprador);
            modelBuilder.Entity<Oferta>()
                .Property(o => o.PrecioVenta)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Favorito>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.IdUser);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}