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
public class ProduccionController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProduccionController(AppDbContext context) => _context = context;

    private async Task<int?> GetLocalIdAsync()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = await _context.Locales.FirstOrDefaultAsync(l => l.UsuarioId == userId);
        return local?.Id;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var producciones = await _context.Producciones
            .Include(p => p.Producto)
            .Include(p => p.Detalles)
                .ThenInclude(d => d.Insumo)
            .Where(p => p.LocalId == localId)
            .OrderByDescending(p => p.Fecha)
            .Select(p => new
            {
                p.Id,
                p.Fecha,
                Producto = new
                {
                    p.Producto.Id,
                    p.Producto.Nombre
                },
                p.CantidadProducida,
                Detalles = p.Detalles.Select(d => new
                {
                    d.InsumoId,
                    InsumoNombre = d.Insumo.Nombre,
                    d.CantidadUtilizada,
                    d.PrecioUnitario,
                    Subtotal = d.CantidadUtilizada * d.PrecioUnitario
                }),
                Total = p.Detalles.Sum(d => d.CantidadUtilizada * d.PrecioUnitario),
                CostoUnitario = p.CantidadProducida > 0
                    ? p.Detalles.Sum(d => d.CantidadUtilizada * d.PrecioUnitario) / p.CantidadProducida
                    : 0
            })
            .ToListAsync();

        return Ok(producciones);
    }


    [HttpPost]
    public async Task<IActionResult> Producir([FromBody] ProduccionDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == dto.ProductoId && p.Categoria.LocalId == localId);
        if (producto == null) return NotFound("Producto no encontrado.");

        if (dto.Detalles == null || !dto.Detalles.Any())
            return BadRequest("Debe especificar los insumos utilizados.");

        decimal costoTotal = 0;
        var detalles = new List<ProduccionDetalle>();

        foreach (var d in dto.Detalles)
        {
            var insumo = await _context.Insumos.FirstOrDefaultAsync(i => i.Id == d.InsumoId && i.LocalId == localId);
            if (insumo == null)
                return BadRequest($"Insumo con ID {d.InsumoId} no encontrado.");
            if (insumo.Stock < d.CantidadUtilizada)
                return BadRequest($"Stock insuficiente para insumo {insumo.Nombre}. Disponible: {insumo.Stock}, requerido: {d.CantidadUtilizada}");

            detalles.Add(new ProduccionDetalle
            {
                InsumoId = insumo.Id,
                CantidadUtilizada = d.CantidadUtilizada,
                PrecioUnitario = insumo.PrecioUnitario
            });

            costoTotal += d.CantidadUtilizada * insumo.PrecioUnitario;
            insumo.Stock -= d.CantidadUtilizada;
        }

        var produccion = new Produccion
        {
            ProductoId = dto.ProductoId,
            CantidadProducida = dto.CantidadProducida,
            Fecha = DateTime.UtcNow,
            LocalId = localId.Value,
            Detalles = detalles
        };

        producto.Stock += dto.CantidadProducida;

        _context.Producciones.Add(produccion);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Mensaje = $"Se produjeron {dto.CantidadProducida} unidades de {producto.Nombre}.",
            NuevoStock = producto.Stock,
            TotalCosto = costoTotal,
            CostoUnitario = dto.CantidadProducida > 0 ? costoTotal / dto.CantidadProducida : 0
        });

    }

    [HttpPost("previsualizar")]
    public async Task<IActionResult> Previsualizar([FromBody] ProduccionDto dto)
    {
        var localId = await GetLocalIdAsync();
        if (localId == null) return Unauthorized();

        if (dto.Detalles == null || !dto.Detalles.Any())
            return BadRequest("Debe especificar los insumos a previsualizar.");

        var detalles = new List<object>();
        decimal total = 0;

        foreach (var d in dto.Detalles)
        {
            var insumo = await _context.Insumos.FirstOrDefaultAsync(i => i.Id == d.InsumoId && i.LocalId == localId);
            if (insumo == null) return BadRequest($"Insumo con ID {d.InsumoId} no encontrado.");

            var subtotal = d.CantidadUtilizada * insumo.PrecioUnitario;
            total += subtotal;

            detalles.Add(new
            {
                Insumo = insumo.Nombre,
                Unidad = insumo.Unidad,
                CantidadUtilizada = d.CantidadUtilizada,
                PrecioUnitario = insumo.PrecioUnitario,
                Subtotal = subtotal
            });
        }

        return Ok(new
        {
            Detalles = detalles,
            TotalCostoEstimado = total
        });
    }
}
