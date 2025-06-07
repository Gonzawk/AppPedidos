namespace AppPedidos.API.DTOs
{
    public class CrearCompraDto
    {
        public int ProveedorId { get; set; }
        public List<CompraDetalleDto> Detalles { get; set; }
    }
}
