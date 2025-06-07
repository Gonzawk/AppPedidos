namespace AppPedidos.API.Models
{
    public class Local
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public string Nombre { get; set; }
        public string Slug { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Coordenadas { get; set; } 

        public string LogoUrl { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

        public List<Categoria> Categorias { get; set; }
        public List<Turno> Turnos { get; set; }

        public ICollection<Modificador> Modificadores { get; set; }

       
        public ICollection<Insumo> Insumos { get; set; }
        public ICollection<Produccion> Producciones { get; set; }
        public ICollection<Proveedor> Proveedores { get; set; }
        public ICollection<Compra> Compras { get; set; }
    }
}
