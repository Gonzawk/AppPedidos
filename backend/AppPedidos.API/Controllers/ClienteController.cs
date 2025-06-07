using AppPedidos.API.Data;
using AppPedidos.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/cliente")]
[Authorize(Roles = "cliente")]
public class ClienteController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClienteController(AppDbContext context)
    {
        _context = context;
    }

    private int GetUsuarioId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    [HttpGet("miperfil")]
    public IActionResult ObtenerPerfil()
    {
        var userId = GetUsuarioId();
        var cliente = _context.Clientes
            .Include(c => c.Usuario)
            .FirstOrDefault(c => c.UsuarioId == userId);

        if (cliente == null) return NotFound();

        return Ok(new ClientePerfilDto
        {
            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido,
            Telefono = cliente.Telefono,
            Email = cliente.Usuario.Email
        });
    }

    [HttpPut("miperfil")]
    public IActionResult ActualizarPerfil([FromBody] ClientePerfilDto dto)
    {
        var userId = GetUsuarioId();
        var cliente = _context.Clientes
            .Include(c => c.Usuario)
            .FirstOrDefault(c => c.UsuarioId == userId);

        if (cliente == null) return NotFound();

        cliente.Nombre = dto.Nombre;
        cliente.Apellido = dto.Apellido;
        cliente.Telefono = dto.Telefono;

        _context.SaveChanges();
        return NoContent();
    }
}
