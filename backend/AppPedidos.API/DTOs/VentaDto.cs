namespace AppPedidos.API.DTOs
{
    public class VentaDto
    {
        public int LocalId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public string MetodoPago { get; set; }
        public int? CuponId { get; set; }
        public List<VentaDetalleDto> Detalles { get; set; }
    }

}
