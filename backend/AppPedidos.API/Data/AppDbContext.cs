using AppPedidos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AppPedidos.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Local> Locales { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Modificador> Modificadores { get; set; }
        public DbSet<ProductoModificador> ProductoModificadores { get; set; }

        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<ProductoInsumo> ProductoInsumos { get; set; }
        public DbSet<Produccion> Producciones { get; set; }
        public DbSet<ProduccionDetalle> ProduccionDetalles { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraDetalle> CompraDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasQueryFilter(u => u.Activo);
            modelBuilder.Entity<Rol>().HasQueryFilter(r => r.Activo);
            modelBuilder.Entity<Cliente>().HasQueryFilter(c => c.Activo);
            modelBuilder.Entity<Direccion>().HasQueryFilter(d => d.Activo);
            modelBuilder.Entity<Local>().HasQueryFilter(l => l.Activo);
            modelBuilder.Entity<Horario>().HasQueryFilter(h => h.Activo);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Cliente)
                .WithOne(c => c.Usuario)
                .HasForeignKey<Cliente>(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Direcciones)
                .WithOne(d => d.Cliente)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Local)
                .WithOne(l => l.Usuario)
                .HasForeignKey<Local>(l => l.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Local>()
                .HasMany(l => l.Turnos)
                .WithOne(t => t.Local)
                .HasForeignKey(t => t.LocalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Local>()
                .HasMany(l => l.Categorias)
                .WithOne(c => c.Local)
                .HasForeignKey(c => c.LocalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Productos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductoModificador>()
                .HasKey(pm => new { pm.ProductoId, pm.ModificadorId });

            modelBuilder.Entity<ProductoModificador>()
                .HasOne(pm => pm.Producto)
                .WithMany(p => p.ProductoModificadores)
                .HasForeignKey(pm => pm.ProductoId);

            modelBuilder.Entity<ProductoModificador>()
                .HasOne(pm => pm.Modificador)
                .WithMany(m => m.ProductoModificadores)
                .HasForeignKey(pm => pm.ModificadorId);

            modelBuilder.Entity<Modificador>()
                .HasOne(m => m.Local)
                .WithMany(l => l.Modificadores)
                .HasForeignKey(m => m.LocalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Modificador>()
                .Property(m => m.PrecioExtra)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Insumo>()
                .Property(i => i.PrecioUnitario)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Insumo>()
                .HasOne(i => i.Local)
                .WithMany(l => l.Insumos)
                .HasForeignKey(i => i.LocalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductoInsumo>()
                .HasKey(pi => new { pi.ProductoId, pi.InsumoId });

            modelBuilder.Entity<ProductoInsumo>()
                .HasOne(pi => pi.Producto)
                .WithMany(p => p.ProductoInsumos)
                .HasForeignKey(pi => pi.ProductoId);

            modelBuilder.Entity<ProductoInsumo>()
                .HasOne(pi => pi.Insumo)
                .WithMany(i => i.ProductoInsumos)
                .HasForeignKey(pi => pi.InsumoId);

            modelBuilder.Entity<Produccion>()
                .HasOne(p => p.Producto)
                .WithMany()
                .HasForeignKey(p => p.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Produccion>()
                .HasOne(p => p.Local)
                .WithMany(l => l.Producciones)
                .HasForeignKey(p => p.LocalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProduccionDetalle>()
                .HasOne(pd => pd.Produccion)
                .WithMany(p => p.Detalles)
                .HasForeignKey(pd => pd.ProduccionId);

            modelBuilder.Entity<ProduccionDetalle>()
                .HasOne(pd => pd.Insumo)
                .WithMany()
                .HasForeignKey(pd => pd.InsumoId);

            modelBuilder.Entity<Proveedor>()
                .HasOne(p => p.Local)
                .WithMany(l => l.Proveedores)
                .HasForeignKey(p => p.LocalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Proveedor)
                .WithMany(p => p.Compras)
                .HasForeignKey(c => c.ProveedorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Local)
                .WithMany(l => l.Compras)
                .HasForeignKey(c => c.LocalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Compra>()
                .Property(c => c.Total)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CompraDetalle>()
                .Property(d => d.PrecioUnitario)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CompraDetalle>()
                .HasOne(cd => cd.Compra)
                .WithMany(c => c.Detalles)
                .HasForeignKey(cd => cd.CompraId);

            modelBuilder.Entity<CompraDetalle>()
                .HasOne(cd => cd.Insumo)
                .WithMany()
                .HasForeignKey(cd => cd.InsumoId);

            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "admin", Activo = true },
                new Rol { Id = 2, Nombre = "local", Activo = true },
                new Rol { Id = 3, Nombre = "cliente", Activo = true }
            );
        }
    }
}
