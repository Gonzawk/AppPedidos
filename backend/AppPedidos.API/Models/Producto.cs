using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppPedidos.API.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; }

        public int Stock { get; set; }
        public bool Activo { get; set; } = true;

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public List<ProductoModificador> ProductoModificadores { get; set; }

        
        public ICollection<ProductoInsumo> ProductoInsumos { get; set; }
    }
}
