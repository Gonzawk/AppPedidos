namespace AppPedidos.API.Models
{
    public class CuponDescuento
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public Local Local { get; set; }

        public string Codigo { get; set; }
        public decimal MontoDescuento { get; set; }

        public int MaximosUsos { get; set; }
        public int UsosActuales { get; set; } = 0;

        public DateTime? FechaExpiracion { get; set; }

        public bool Activo => (FechaExpiracion == null || FechaExpiracion > DateTime.UtcNow)
                           && UsosActuales < MaximosUsos;
    }

}
