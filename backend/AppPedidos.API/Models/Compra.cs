namespace AppPedidos.API.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public int ProveedorId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }

        public Proveedor Proveedor { get; set; }
        public Local Local { get; set; }
        public ICollection<CompraDetalle> Detalles { get; set; }
    }
}
