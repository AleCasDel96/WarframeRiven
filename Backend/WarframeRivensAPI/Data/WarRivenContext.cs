﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarframeRivensAPI.Models;

namespace WarframeRivensAPI.Data
{
    public partial class WarRivenContext : IdentityDbContext<WarUser>
    {
        public WarRivenContext(DbContextOptions<WarRivenContext> options) : base(options) { }
        public virtual DbSet<Riven> Rivens { get; set; }
        public virtual DbSet<Oferta> Ofertas { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<WarUser> users = new List<WarUser>
            {
                new WarUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "visitante@warriven.com",
                    NormalizedEmail = "VISITANTE@WARRIVEN.COM",
                    UserName = "visitante@warriven.com",
                    NormalizedUserName = "VISITANTE@WARRIVEN.COM",
                    EmailConfirmed = true,
                    Nickname = "Usuario Visitante",
                },
                new WarUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "confirmado@warriven.com",
                    NormalizedEmail = "CONFIRMADO@WARRIVEN.COM",
                    UserName = "confirmado@warriven.com",
                    NormalizedUserName = "CONFIRMADO@WARRIVEN.COM",
                    EmailConfirmed = true,
                    Nickname = "Usuario confirmado",
                },
                new WarUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "admin@warriven.com",
                    NormalizedEmail = "ADMIN@WARRIVEN.COM",
                    UserName = "admin@warriven.com",
                    NormalizedUserName = "ADMIN@WARRIVEN.COM",
                    EmailConfirmed = true,
                    Nickname = "Administrador",
                },
            };
            PasswordHasher<WarUser> hasher = new PasswordHasher<WarUser>();
            users[0].PasswordHash = hasher.HashPassword(users[0], "Riven1234!");
            users[1].PasswordHash = hasher.HashPassword(users[1], "Riven1234!");
            users[2].PasswordHash = hasher.HashPassword(users[2], "Riven1234!");
            modelBuilder.Entity<WarUser>().HasData(users);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole {Id = Guid.NewGuid().ToString(), Name="basic", NormalizedName="BASIC" },
                new IdentityRole {Id = Guid.NewGuid().ToString(), Name="confirmado", NormalizedName="CONFIRMADO" },
                new IdentityRole {Id = Guid.NewGuid().ToString(), Name="admin", NormalizedName="ADMIN" },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { UserId = users[0].Id, RoleId = roles[0].Id },
                new IdentityUserRole<string> { UserId = users[1].Id, RoleId = roles[1].Id },
                new IdentityUserRole<string> { UserId = users[2].Id, RoleId = roles[2].Id }
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            modelBuilder.Entity<Riven>().HasOne(r => r.Propietario).WithMany().HasForeignKey(r => r.IdPropietario).HasPrincipalKey(u => u.Id);
            modelBuilder.Entity<Riven>().Property(r => r.Valor1).HasPrecision(4, 1);
            modelBuilder.Entity<Riven>().Property(r => r.Valor2).HasPrecision(4, 1);
            modelBuilder.Entity<Riven>().Property(r => r.Valor3).HasPrecision(4, 1);
            modelBuilder.Entity<Riven>().Property(r => r.DValor).HasPrecision(4, 1);
            modelBuilder.Entity<Venta>().HasOne(v => v.Comprador).WithMany().HasForeignKey(v => v.IdComprador).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Venta>().HasOne(v => v.Vendedor).WithMany().HasForeignKey(v => v.IdVendedor).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Oferta>().HasOne(o => o.Vendedor).WithMany().HasForeignKey(o => o.IdVendedor);
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}