namespace AppPedidos.API.DTOs
{
    public class ProduccionDetalleDto
    {
        public int InsumoId { get; set; }
        public string InsumoNombre { get; set; } // opcional, útil para mostrar
        public decimal CantidadUtilizada { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
