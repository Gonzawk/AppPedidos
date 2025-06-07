namespace AppPedidos.API.DTOs
{
    public class ProductoInsumoDto
    {
        public int ProductoId { get; set; }
        public int InsumoId { get; set; }
        public string NombreInsumo { get; set; }
        public decimal CantidadPorUnidad { get; set; }
    }

}
