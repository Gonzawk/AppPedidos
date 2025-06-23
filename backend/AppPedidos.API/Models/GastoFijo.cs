namespace AppPedidos.API.Models
{
    public class GastoFijo
    {
        public int Id { get; set; }

        public int LocalId { get; set; }
        public Local Local { get; set; }

        public string Nombre { get; set; }               // Ej: Sueldo, Luz
        public decimal MontoMensual { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }

}
