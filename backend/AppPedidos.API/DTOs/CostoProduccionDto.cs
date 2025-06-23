using System;

namespace AppPedidos.API.DTOs
{
    public class CostoProduccionDto
    {
        public int ProduccionId { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal MargenGanancia { get; set; }
        public decimal PrecioSugerido { get; set; }
        public DateTime FechaCalculo { get; set; }
    }
}
