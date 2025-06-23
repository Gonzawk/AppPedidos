namespace AppPedidos.API.DTOs
{
    public class CajaDiariaDto
    {
        public int Id { get; set; }  
        public int LocalId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal TotalFijosProrrateado { get; set; }
        public decimal CajaFinal => TotalVentas - (TotalGastos + TotalFijosProrrateado);
    }
}
