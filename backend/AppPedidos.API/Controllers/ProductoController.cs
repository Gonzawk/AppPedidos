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
public class ProductoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductoController(AppDbContext context)
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
    public async Task<ActionResult<IEnumerable<ProductoDto>>> GetAll()
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var productos = await _context.Productos
            .Include(p => p.Categoria)
            .Include(p => p.ProductoModificadores)
                .ThenInclude(pm => pm.Modificador)
            .Where(p => p.Categoria.LocalId == localId)
            .Select(p => new ProductoDto
            {
                Id = p.Id,
                CategoriaId = p.CategoriaId,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                ImagenUrl = p.ImagenUrl,
                Activo = p.Activo,
                Modificadores = p.ProductoModificadores.Select(pm => new ModificadorDto
                {
                    Id = pm.Modificador.Id,
                    Nombre = pm.Modificador.Nombre,
                    PrecioExtra = pm.Modificador.PrecioExtra
                }).ToList()
            })
            .ToListAsync();

        return Ok(productos);
    }

    [HttpPost]
    public async Task<ActionResult<ProductoDto>> Crear([FromBody] ProductoDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == dto.CategoriaId && c.LocalId == localId);
        if (categoria == null) return BadRequest("Categoría inválida.");

        var producto = new Producto
        {
            CategoriaId = dto.CategoriaId,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            ImagenUrl = dto.ImagenUrl,
            Activo = true,
            Stock = 0
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        dto.Id = producto.Id;
        return CreatedAtAction(nameof(GetAll), new { id = producto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ProductoDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id && p.Categoria.LocalId == localId);
        if (producto == null) return NotFound();

        var nuevaCategoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == dto.CategoriaId && c.LocalId == localId);
        if (nuevaCategoria == null) return BadRequest("Categoría inválida.");

        producto.Nombre = dto.Nombre;
        producto.Descripcion = dto.Descripcion;
        producto.Precio = dto.Precio;
        producto.ImagenUrl = dto.ImagenUrl;
        producto.CategoriaId = dto.CategoriaId;

        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id && p.Categoria.LocalId == localId);
        if (producto == null) return NotFound();

        producto.Activo = false;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/modificadores")]
    public async Task<ActionResult<IEnumerable<ModificadorDto>>> GetModificadores(int id)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var producto = await _context.Productos
            .Include(p => p.ProductoModificadores)
                .ThenInclude(pm => pm.Modificador)
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id && p.Categoria.LocalId == localId);

        if (producto == null) return NotFound();

        var modificadores = producto.ProductoModificadores.Select(pm => new ModificadorDto
        {
            Id = pm.Modificador.Id,
            Nombre = pm.Modificador.Nombre,
            PrecioExtra = pm.Modificador.PrecioExtra
        });

        return Ok(modificadores);
    }

    [HttpPost("{id}/modificadores")]
    public async Task<IActionResult> AsignarModificadores(int id, [FromBody] List<int> modificadorIds)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var producto = await _context.Productos
            .Include(p => p.ProductoModificadores)
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id && p.Categoria.LocalId == localId);

        if (producto == null) return NotFound();

        producto.ProductoModificadores.Clear();

        foreach (var modId in modificadorIds)
        {
            producto.ProductoModificadores.Add(new ProductoModificador
            {
                ProductoId = id,
                ModificadorId = modId
            });
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }
}

