namespace AppPedidos.API.DTOs
{
    public class InsumoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Unidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Stock { get; set; }
    }

}
