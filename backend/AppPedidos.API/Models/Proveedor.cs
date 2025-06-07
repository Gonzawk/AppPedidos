namespace AppPedidos.API.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        public int LocalId { get; set; }

        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        public Local Local { get; set; }
        public ICollection<Compra> Compras { get; set; }
    }
}
