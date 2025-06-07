using AppPedidos.API.Models;

public class ProduccionDetalle
{
    public int Id { get; set; }
    public int ProduccionId { get; set; }
    public Produccion Produccion { get; set; }

    public int InsumoId { get; set; }
    public Insumo Insumo { get; set; }

    public decimal CantidadUtilizada { get; set; }
    public decimal PrecioUnitario { get; set; }
}
