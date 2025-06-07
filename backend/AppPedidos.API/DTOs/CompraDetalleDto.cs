namespace AppPedidos.API.DTOs
{
    public class CompraDetalleDto
    {
        public int InsumoId { get; set; }
        public string? InsumoNombre { get; set; } 
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
