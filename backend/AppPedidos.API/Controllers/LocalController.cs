using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AppPedidos.API.Data;
using AppPedidos.API.Models;
using AppPedidos.API.DTOs;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "local")]
public class LocalController : ControllerBase
{
    private readonly AppDbContext _context;

    public LocalController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPut]
    public IActionResult ActualizarLocal([FromBody] LocalUpdateDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = _context.Locales.FirstOrDefault(l => l.UsuarioId == userId);

        if (local == null)
            return NotFound("No tenés un local para actualizar.");

        local.Nombre = dto.Nombre;
        local.Telefono = dto.Telefono;
        local.Direccion = dto.Direccion;
        local.Coordenadas = dto.Coordenadas;
        local.LogoUrl = dto.LogoUrl;

        // Slug y activación del local si completa datos
        local.Slug = dto.Nombre
            .ToLower()
            .Replace(" ", "-")
            .Replace("á", "a")
            .Replace("é", "e")
            .Replace("í", "i")
            .Replace("ó", "o")
            .Replace("ú", "u");

        // Activar local si todos los campos esenciales están llenos
        local.Activo = !string.IsNullOrEmpty(local.Nombre) && !string.IsNullOrEmpty(local.Direccion);

        _context.SaveChanges();
        return Ok("Local actualizado correctamente.");
    }

    [HttpGet("mio")]
    public IActionResult ObtenerMiLocal()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var local = _context.Locales.FirstOrDefault(l => l.UsuarioId == userId);

        if (local == null)
            return NotFound("No tenés local registrado.");

        return Ok(local);
    }
}
