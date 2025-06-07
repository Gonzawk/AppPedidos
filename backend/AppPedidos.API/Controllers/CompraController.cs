using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AppPedidos.API.Data;
using AppPedidos.API.DTOs;
using AppPedidos.API.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "local")]
public class CompraController : ControllerBase
{
    private readonly AppDbContext _context;
    public CompraController(AppDbContext ctx) => _context = ctx;

    private async Task<int?> GetLocalIdAsync()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = await _context.Locales.FirstOrDefaultAsync(l => l.UsuarioId == userId);
        return local?.Id;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompraDto>>> Get()
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var compras = await _context.Compras
            .Include(c => c.Proveedor)
            .Where(c => c.LocalId == localId)
            .OrderByDescending(c => c.Fecha)
            .Select(c => new CompraDto
            {
                Id = c.Id,
                Fecha = c.Fecha,
                ProveedorId = c.ProveedorId,
                ProveedorNombre = c.Proveedor.Nombre,
                Total = c.Total
            }).ToListAsync();

        return Ok(compras);
    }

    [HttpPost]
    public async Task<ActionResult<CompraDto>> Crear([FromBody] CrearCompraDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var compra = new Compra
        {
            LocalId = localId.Value,
            ProveedorId = dto.ProveedorId,
            Fecha = DateTime.UtcNow,
            Total = 0,
            Detalles = new List<CompraDetalle>()
        };

        foreach (var d in dto.Detalles)
        {
            var insumo = await _context.Insumos
                .FirstOrDefaultAsync(i => i.Id == d.InsumoId && i.LocalId == localId);
            if (insumo == null)
                return BadRequest($"Insumo con ID {d.InsumoId} no encontrado.");

            var detalle = new CompraDetalle
            {
                InsumoId = d.InsumoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario
            };

            compra.Detalles.Add(detalle);

            insumo.Stock += d.Cantidad;
            insumo.PrecioUnitario = d.PrecioUnitario;

            compra.Total += d.Cantidad * d.PrecioUnitario;
        }

        _context.Compras.Add(compra);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = compra.Id }, new CompraDto
        {
            Id = compra.Id,
            Fecha = compra.Fecha,
            ProveedorId = compra.ProveedorId,
            ProveedorNombre = (await _context.Proveedores.FindAsync(compra.ProveedorId))?.Nombre,
            Total = compra.Total
        });
    }

}
