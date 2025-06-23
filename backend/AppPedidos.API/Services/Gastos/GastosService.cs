using AppPedidos.API.Data;
using AppPedidos.API.DTOs;
using AppPedidos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AppPedidos.API.Services.Gastos
{
    public class GastosService : IGastosService
    {
        private readonly AppDbContext _context;

        public GastosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarGastoFijoAsync(GastoFijoDto dto)
        {
            var gastoFijo = new GastoFijo
            {
                LocalId = dto.LocalId,
                Nombre = dto.Nombre,
                MontoMensual = dto.MontoMensual,
                FechaRegistro = DateTime.UtcNow
            };

            _context.GastosFijos.Add(gastoFijo);
            await _context.SaveChangesAsync();
        }

        public async Task RegistrarGastoVariableAsync(GastoVariableDto dto)
        {
            var gastoVariable = new GastoVariable
            {
                LocalId = dto.LocalId,
                Motivo = dto.Motivo,
                Monto = dto.Monto,
                Fecha = dto.Fecha
            };

            _context.GastosVariables.Add(gastoVariable);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GastoFijo>> ObtenerGastosFijosAsync(int localId)
        {
            return await _context.GastosFijos
                .Where(g => g.LocalId == localId)
                .ToListAsync();
        }

        public async Task<List<GastoVariable>> ObtenerGastosVariablesAsync(int localId, DateTime fecha)
        {
            return await _context.GastosVariables
                .Where(g => g.LocalId == localId && g.Fecha.Date == fecha.Date)
                .ToListAsync();
        }
    }
}
