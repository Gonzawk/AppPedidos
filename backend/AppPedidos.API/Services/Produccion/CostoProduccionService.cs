using AppPedidos.API.Data;
using AppPedidos.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AppPedidos.API.Services.Produccion
{
    public class CostoProduccionService : ICostoProduccionService
    {
        private readonly AppDbContext _context;

        public CostoProduccionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CostoProduccionDto> CalcularCostoProduccionAsync(int produccionId)
        {
            var costo = await _context.CostosProduccionProducto
                .Include(c => c.Produccion)
                .FirstOrDefaultAsync(c => c.ProduccionId == produccionId);

            if (costo == null) return null;

            return new CostoProduccionDto
            {
                ProduccionId = costo.ProduccionId,
                CostoUnitario = costo.CostoUnitario,
                MargenGanancia = costo.MargenGanancia,
                PrecioSugerido = costo.PrecioSugerido,
                FechaCalculo = costo.FechaCalculo
            };
        }

        public async Task<decimal> ObtenerCostoProduccionDelDiaAsync(int localId, DateTime fecha)
        {
            var costos = await _context.CostosProduccionProducto
                .Include(c => c.Produccion)
                .Where(c => c.Produccion.LocalId == localId && c.FechaCalculo.Date == fecha.Date)
                .ToListAsync();

            return costos.Sum(c => c.CostoUnitario * c.Produccion.CantidadProducida);
        }
    }
}


