using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppPedidos.API.Data;
using AppPedidos.API.DTOs;
using AppPedidos.API.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "local")]
public class DetalleCompraController : ControllerBase
{
    private readonly AppDbContext _context;
    public DetalleCompraController(AppDbContext ctx) => _context = ctx;

    [HttpGet("{compraId}")]
    public async Task<ActionResult<IEnumerable<CompraDetalleDto>>> Get(int compraId)
    {
        var detalles = await _context.CompraDetalles
            .Include(d => d.Insumo)
            .Where(d => d.CompraId == compraId)
            .Select(d => new CompraDetalleDto
            {
                InsumoId = d.InsumoId,
                InsumoNombre = d.Insumo.Nombre,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario
            }).ToListAsync();

        return Ok(detalles);
    }

    [HttpPost("{compraId}")]
    public async Task<IActionResult> Agregar(int compraId, [FromBody] List<CompraDetalleDto> detallesDto)
    {
        var compra = await _context.Compras.Include(c => c.Detalles).FirstOrDefaultAsync(c => c.Id == compraId);
        if (compra == null) return NotFound();

        foreach (var dto in detallesDto)
        {
            var insumo = await _context.Insumos.FindAsync(dto.InsumoId);
            if (insumo == null) continue;

            var detalle = new CompraDetalle
            {
                CompraId = compraId,
                InsumoId = dto.InsumoId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario
            };

            _context.CompraDetalles.Add(detalle);

            insumo.Stock += dto.Cantidad;
            insumo.PrecioUnitario = dto.PrecioUnitario;
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
