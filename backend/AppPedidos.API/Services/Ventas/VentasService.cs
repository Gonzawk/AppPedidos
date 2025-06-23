using AppPedidos.API.Data;
using AppPedidos.API.DTOs;
using AppPedidos.API.Models;
using AppPedidos.API.Services.Ventas;
using Microsoft.EntityFrameworkCore;

public class VentasService : IVentasService
{
    private readonly AppDbContext _context;

    public VentasService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> RegistrarVentaAsync(VentaDto dto)
    {
        // Aplicar lógica del cupón si existe
        CuponDescuento? cupon = null;
        if (dto.CuponId.HasValue)
        {
            cupon = await _context.CuponesDescuento.FindAsync(dto.CuponId.Value);
            if (cupon == null || cupon.FechaExpiracion < DateTime.UtcNow || cupon.UsosActuales >= cupon.MaximosUsos)
                throw new Exception("Cupón inválido");

            cupon.UsosActuales += 1;
        }

        // Crear venta
        var venta = new Venta
        {
            LocalId = dto.LocalId,
            Fecha = dto.Fecha,
            Subtotal = dto.Subtotal,
            Descuento = dto.Descuento,
            MetodoPago = dto.MetodoPago,
            CuponId = dto.CuponId,
            Detalles = dto.Detalles.Select(d => new VentaDetalle
            {
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario
            }).ToList()
        };

        await _context.Ventas.AddAsync(venta);
        await _context.SaveChangesAsync();

        return venta.Id;
    }

    public async Task<List<Venta>> ObtenerVentasPorFechaAsync(int localId, DateTime fecha)
    {
        return await _context.Ventas
            .Include(v => v.Detalles)
            .Where(v => v.LocalId == localId && v.Fecha.Date == fecha.Date)
            .ToListAsync();
    }
}
