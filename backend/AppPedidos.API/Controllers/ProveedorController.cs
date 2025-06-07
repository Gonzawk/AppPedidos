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
public class ProveedorController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProveedorController(AppDbContext ctx) => _context = ctx;

    private async Task<int?> GetLocalIdAsync()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = await _context.Locales.FirstOrDefaultAsync(l => l.UsuarioId == userId);
        return local?.Id;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get()
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var proveedores = await _context.Proveedores
            .Where(p => p.LocalId == localId)
            .Select(p => new ProveedorDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Email = p.Email,
                Direccion = p.Direccion
            }).ToListAsync();

        return Ok(proveedores);
    }

    [HttpPost]
    public async Task<ActionResult<ProveedorDto>> Crear([FromBody] ProveedorDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var proveedor = new Proveedor
        {
            LocalId = localId.Value,
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            Email = dto.Email,
            Direccion = dto.Direccion
        };

        _context.Proveedores.Add(proveedor);
        await _context.SaveChangesAsync();

        dto.Id = proveedor.Id;
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }
}
