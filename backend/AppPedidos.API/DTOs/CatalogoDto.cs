namespace AppPedidos.API.DTOs
{
    public class CatalogoDTO
    {
        public int LocalId { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Coordenadas { get; set; }
        public string LogoUrl { get; set; }
        public List<TurnoCatalogoDTO> TurnosDisponibles { get; set; }
        public List<ProductoCatalogoDTO> Productos { get; set; }
    }

}
