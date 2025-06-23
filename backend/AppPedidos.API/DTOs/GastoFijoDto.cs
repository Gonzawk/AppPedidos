namespace AppPedidos.API.DTOs
{
    public class GastoFijoDto
    {
        public int LocalId { get; set; }
        public string Nombre { get; set; }
        public decimal MontoMensual { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

}
