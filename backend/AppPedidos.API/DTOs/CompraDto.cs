using System;
using System.Collections.Generic;

namespace AppPedidos.API.DTOs
{
    public class CompraDto
    {
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public List<CompraDetalleDto> Detalles { get; set; } = new();
    }
}
