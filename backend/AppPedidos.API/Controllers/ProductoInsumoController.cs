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
public class ProductoInsumoController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductoInsumoController(AppDbContext context) => _context = context;

    private async Task<int?> GetLocalIdAsync()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = await _context.Locales.FirstOrDefaultAsync(l => l.UsuarioId == userId);
        return local?.Id;
    }

    [HttpGet("{productoId}")]
    public async Task<ActionResult<IEnumerable<ProductoInsumoDto>>> Get(int productoId)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == productoId && p.Categoria.LocalId == localId);

        if (producto == null) return NotFound();

        var relaciones = await _context.ProductoInsumos
            .Where(pi => pi.ProductoId == productoId)
            .Include(pi => pi.Insumo)
            .ToListAsync();

        var resultado = relaciones.Select(pi => new ProductoInsumoDto
        {
            ProductoId = pi.ProductoId,
            InsumoId = pi.InsumoId,
            NombreInsumo = pi.Insumo.Nombre,
            CantidadPorUnidad = pi.CantidadPorUnidad
        });

        return Ok(resultado);
    }

    [HttpPost("{productoId}")]
    public async Task<IActionResult> Asignar(int productoId, [FromBody] List<ProductoInsumoDto> dtos)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == productoId && p.Categoria.LocalId == localId);
        if (producto == null) return NotFound();

        var existentes = await _context.ProductoInsumos.Where(pi => pi.ProductoId == productoId).ToListAsync();
        _context.ProductoInsumos.RemoveRange(existentes);

        foreach (var dto in dtos)
        {
            _context.ProductoInsumos.Add(new ProductoInsumo
            {
                ProductoId = productoId,
                InsumoId = dto.InsumoId,
                CantidadPorUnidad = dto.CantidadPorUnidad
            });
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
