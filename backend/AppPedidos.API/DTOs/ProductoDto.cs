using AppPedidos.API.DTOs;

public class ProductoDto
{
    public int Id { get; set; }
    public int CategoriaId { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string ImagenUrl { get; set; }
    public bool Activo { get; set; }

    public List<ModificadorDto> Modificadores { get; set; } = new();
}
