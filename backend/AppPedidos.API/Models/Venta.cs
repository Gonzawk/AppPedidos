namespace AppPedidos.API.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public Local Local { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; } = 0;
        public decimal Total => Subtotal - Descuento;

        public string MetodoPago { get; set; }

        public int? CuponId { get; set; }
        public CuponDescuento Cupon { get; set; }

        public ICollection<VentaDetalle> Detalles { get; set; }
    }

}
