namespace AppPedidos.API.DTOs
{
    public class ProduccionDto
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int CantidadProducida { get; set; }
        public DateTime Fecha { get; set; }
        public List<ProduccionDetalleDto> Detalles { get; set; }
    }
}