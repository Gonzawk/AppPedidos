namespace AppPedidos.API.DTOs
{
    public class ProductoCatalogoDTO
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; }
        public List<ModificadorCatalogoDTO> Modificadores { get; set; }
    }


}
