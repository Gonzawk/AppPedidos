namespace AppPedidos.API.Models
{
    public class Insumo
    {
        public int Id { get; set; }
        public int LocalId { get; set; }

        public string Nombre { get; set; }
        public string Unidad { get; set; }
        public decimal PrecioUnitario { get; set; } = 0;
        public decimal Stock { get; set; } = 0;

        public Local Local { get; set; }

       
        public ICollection<ProductoInsumo> ProductoInsumos { get; set; }
        
    }
}
