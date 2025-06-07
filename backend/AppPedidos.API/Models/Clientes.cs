using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppPedidos.API.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public bool Activo { get; set; } = true;

        public List<Direccion> Direcciones { get; set; }
    }

}
