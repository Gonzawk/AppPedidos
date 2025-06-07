using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppPedidos.API.Models
{
    public class Modificador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioExtra { get; set; }

        
        public int LocalId { get; set; }
        public Local Local { get; set; }

        public ICollection<ProductoModificador> ProductoModificadores { get; set; }
    }

}
