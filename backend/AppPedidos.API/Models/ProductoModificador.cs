namespace AppPedidos.API.Models
{
    public class ProductoModificador
    {
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int ModificadorId { get; set; }
        public Modificador Modificador { get; set; }
    }
}
