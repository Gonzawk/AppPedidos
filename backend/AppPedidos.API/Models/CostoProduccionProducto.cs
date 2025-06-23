namespace AppPedidos.API.Models
{
    public class CostoProduccionProducto
    {
        public int Id { get; set; }

        public int ProduccionId { get; set; }
        public Produccion Produccion { get; set; }

        public decimal CostoUnitario { get; set; }       // Calculado automáticamente tras producir

        public decimal MargenGanancia { get; set; } = 0.30m; // Porcentaje (ej: 0.30 = 30%)

        public decimal PrecioSugerido => Math.Round(CostoUnitario * (1 + MargenGanancia), 2);

        public DateTime FechaCalculo { get; set; } = DateTime.UtcNow;
    }

}
