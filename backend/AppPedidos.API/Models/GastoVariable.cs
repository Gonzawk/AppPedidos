namespace AppPedidos.API.Models
{
    public class GastoVariable
    {
        public int Id { get; set; }

        public int LocalId { get; set; }
        public Local Local { get; set; }

        public string Motivo { get; set; }               // Ej: Arreglo, Reposición, Publicidad
        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }

}
