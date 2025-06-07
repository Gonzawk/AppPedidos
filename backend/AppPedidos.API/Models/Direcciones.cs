namespace AppPedidos.API.Models
{
    public class Direccion
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string Coordenadas { get; set; }

        public bool Principal { get; set; } = false;
        public bool Activo { get; set; } = true;
    }

}
