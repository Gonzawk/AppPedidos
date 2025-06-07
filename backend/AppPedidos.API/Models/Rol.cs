using System.ComponentModel.DataAnnotations;

namespace AppPedidos.API.Models
{
    public class Rol
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } 

        public bool Activo { get; set; } = true;

        public List<Usuario> Usuarios { get; set; }
    }

}
