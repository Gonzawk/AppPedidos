namespace AppPedidos.API.DTOs
{
    public class GastoVariableDto
    {
        public int LocalId { get; set; }
        public string Motivo { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }

}
