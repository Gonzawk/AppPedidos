namespace AppPedidos.API.DTOs
{
    public class VentaDetalleDto
    {
        public int ProductoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

}
