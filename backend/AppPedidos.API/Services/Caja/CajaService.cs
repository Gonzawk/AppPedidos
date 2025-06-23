using AppPedidos.API.Data;
using AppPedidos.API.DTOs;
using AppPedidos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AppPedidos.API.Services.Caja
{
    public class CajaService : ICajaService
    {
        private readonly AppDbContext _context;

        public CajaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CajaDiariaDto> CalcularCajaDiariaAsync(int localId, DateTime fecha)
        {
            var ventas = await _context.Ventas
                .Where(v => v.LocalId == localId && v.Fecha.Date == fecha.Date)
                .ToListAsync();

            var gastosVariables = await _context.GastosVariables
                .Where(g => g.LocalId == localId && g.Fecha.Date == fecha.Date)
                .ToListAsync();

            var gastosFijos = await _context.GastosFijos
                .Where(g => g.LocalId == localId)
                .ToListAsync();

            decimal totalVentas = ventas.Sum(v => v.Subtotal - v.Descuento);
            decimal totalGastos = gastosVariables.Sum(g => g.Monto);
            decimal gastosFijosProrrateado = gastosFijos.Sum(g => g.MontoMensual) / 30;

            return new CajaDiariaDto
            {
                LocalId = localId,
                Fecha = fecha,
                TotalVentas = totalVentas,
                TotalGastos = totalGastos,
                TotalFijosProrrateado = Math.Round(gastosFijosProrrateado, 2)
            };
        }

        public async Task<List<CajaDiariaDto>> ObtenerCajasPorRangoFechaAsync(int localId, DateTime desde, DateTime hasta)
        {
            return await _context.CajasDiarias
                .Where(c => c.LocalId == localId && c.Fecha >= desde && c.Fecha <= hasta)
                .Select(c => new CajaDiariaDto
                {
                    Id = c.Id,
                    LocalId = c.LocalId,
                    Fecha = c.Fecha,
                    TotalVentas = c.TotalVentas,
                    TotalGastos = c.TotalGastos,
                    TotalFijosProrrateado = c.TotalFijosProrrateado
                }).ToListAsync();
        }
    }
}

