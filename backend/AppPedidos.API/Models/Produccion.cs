namespace AppPedidos.API.Models
{
    public class Produccion
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int CantidadProducida { get; set; }
        public DateTime Fecha { get; set; }
        public int LocalId { get; set; }
        public Local Local { get; set; }
        public ICollection<ProduccionDetalle> Detalles { get; set; }
    }

}
