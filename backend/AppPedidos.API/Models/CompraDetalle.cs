namespace AppPedidos.API.Models
{
    public class CompraDetalle
    {
        public int Id { get; set; }
        public int CompraId { get; set; }
        public int InsumoId { get; set; }

        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;

        public Compra Compra { get; set; }
        public Insumo Insumo { get; set; }
    }
}
