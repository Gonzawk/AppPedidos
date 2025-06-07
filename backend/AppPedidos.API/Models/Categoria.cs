using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppPedidos.API.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public int LocalId { get; set; }
        public Local Local { get; set; }

        public bool Activo { get; set; } = true;

        public List<Producto> Productos { get; set; }
    }
}
