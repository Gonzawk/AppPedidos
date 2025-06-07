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
public class InsumoController : ControllerBase
{
    private readonly AppDbContext _context;
    public InsumoController(AppDbContext ctx) => _context = ctx;

    private async Task<int?> GetLocalIdAsync()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = await _context.Locales.FirstOrDefaultAsync(l => l.UsuarioId == userId);
        return local?.Id;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InsumoDto>>> Get()
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var insumos = await _context.Insumos
            .Where(i => i.LocalId == localId)
            .Select(i => new InsumoDto
            {
                Id = i.Id,
                Nombre = i.Nombre,
                Unidad = i.Unidad,
                PrecioUnitario = i.PrecioUnitario,
                Stock = i.Stock
            }).ToListAsync();

        return Ok(insumos);
    }

    [HttpPost]
    public async Task<ActionResult<InsumoDto>> Crear([FromBody] CrearInsumoDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var insumo = new Insumo
        {
            LocalId = localId.Value,
            Nombre = dto.Nombre,
            Unidad = dto.Unidad
        };

        _context.Insumos.Add(insumo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = insumo.Id }, new InsumoDto
        {
            Id = insumo.Id,
            Nombre = insumo.Nombre,
            Unidad = insumo.Unidad,
            Stock = insumo.Stock,
            PrecioUnitario = insumo.PrecioUnitario
        });
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] CrearInsumoDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var insumo = await _context.Insumos
            .FirstOrDefaultAsync(i => i.Id == id && i.LocalId == localId);
        if (insumo == null) return NotFound();

        insumo.Nombre = dto.Nombre;
        insumo.Unidad = dto.Unidad;

        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var insumo = await _context.Insumos
            .FirstOrDefaultAsync(i => i.Id == id && i.LocalId == localId);
        if (insumo == null) return NotFound();

        _context.Insumos.Remove(insumo);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
