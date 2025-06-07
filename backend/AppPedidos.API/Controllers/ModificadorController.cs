using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AppPedidos.API.Data;
using AppPedidos.API.Models;
using AppPedidos.API.DTOs;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "local")]
public class ModificadorController : ControllerBase
{
    private readonly AppDbContext _context;

    public ModificadorController(AppDbContext context)
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
    public async Task<ActionResult<IEnumerable<ModificadorDto>>> Get()
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var modificadores = await _context.Modificadores
            .Where(m => m.LocalId == localId)
            .Select(m => new ModificadorDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                PrecioExtra = m.PrecioExtra
            })
            .ToListAsync();

        return Ok(modificadores);
    }

    [HttpPost]
    public async Task<ActionResult<ModificadorDto>> Crear([FromBody] ModificadorDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var modificador = new Modificador
        {
            Nombre = dto.Nombre,
            PrecioExtra = dto.PrecioExtra,
            LocalId = localId.Value
        };

        _context.Modificadores.Add(modificador);
        await _context.SaveChangesAsync();

        dto.Id = modificador.Id;
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ModificadorDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var modificador = await _context.Modificadores
            .FirstOrDefaultAsync(m => m.Id == id && m.LocalId == localId);

        if (modificador == null) return NotFound();

        modificador.Nombre = dto.Nombre;
        modificador.PrecioExtra = dto.PrecioExtra;

        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var modificador = await _context.Modificadores
            .FirstOrDefaultAsync(m => m.Id == id && m.LocalId == localId);
        if (modificador == null) return NotFound(); 

        _context.Modificadores.Remove(modificador);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
