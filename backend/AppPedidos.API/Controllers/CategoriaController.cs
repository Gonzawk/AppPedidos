using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AppPedidos.API.Data;
using AppPedidos.API.Models;
using AppPedidos.API.DTOs;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "local")]
public class CategoriaController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriaController(AppDbContext context)
    {
        _context = context;
    }

    private async Task<int?> GetLocalIdAsync()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = await _context.Locales.FirstOrDefaultAsync(l => l.UsuarioId == userId);
        return local?.Id;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> Get()
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var categorias = await _context.Categorias
            .Where(c => c.LocalId == localId)
            .Select(c => new CategoriaDto { Id = c.Id, Nombre = c.Nombre })
            .ToListAsync();

        return Ok(categorias);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> Crear([FromBody] CategoriaDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var categoria = new Categoria
        {
            Nombre = dto.Nombre,
            LocalId = localId.Value,
            Activo = true
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        dto.Id = categoria.Id;
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] CategoriaDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id && c.LocalId == localId);
        if (categoria == null) return NotFound();

        categoria.Nombre = dto.Nombre;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id && c.LocalId == localId);
        if (categoria == null) return NotFound();

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
