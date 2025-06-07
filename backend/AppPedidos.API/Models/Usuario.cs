using System.ComponentModel.DataAnnotations;

namespace AppPedidos.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public Cliente Cliente { get; set; }
         public Local Local { get; set; }
    }

}
