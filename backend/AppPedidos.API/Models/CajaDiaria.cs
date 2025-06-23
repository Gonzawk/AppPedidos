namespace AppPedidos.API.Models
{
    public class CajaDiaria
    {
        public int Id { get; set; }

        public int LocalId { get; set; }
        public Local Local { get; set; }

        public DateTime Fecha { get; set; }

        public decimal TotalVentas { get; set; }       // Suma total de ventas del día
        public decimal TotalGastos { get; set; }       // GastoVariable del día
        public decimal TotalFijosProrrateado { get; set; } // GastoFijo/30 si aplica
        public decimal CajaFinal => TotalVentas - (TotalGastos + TotalFijosProrrateado);
    }

}
