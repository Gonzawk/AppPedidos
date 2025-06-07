using AppPedidos.API.Data;
using AppPedidos.API.DTOs;
using AppPedidos.API.Helpers;
using AppPedidos.API.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtSettings _jwtSettings;

    public AuthController(AppDbContext context, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value;
    }
    // Registro de clientes
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("El email ya está registrado.");

        var usuario = new Usuario
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            RolId = 3, // siempre cliente
            Activo = true,
            FechaCreacion = DateTime.UtcNow
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        var cliente = new Cliente
        {
            UsuarioId = usuario.Id,
            Nombre = "",
            Apellido = "",
            Telefono = ""
        };
        _context.Clientes.Add(cliente);

        await _context.SaveChangesAsync();

        return Ok("Usuario registrado correctamente.");
    }


    [HttpPost("registro-local")]
    public async Task<IActionResult> RegistroLocal([FromBody] RegistroLocalDto dto)
    {
        if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("El email ya está registrado.");

        var usuario = new Usuario
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            RolId = 2, // fijo para local
            Activo = true,
            FechaCreacion = DateTime.UtcNow
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        var slug = dto.Nombre
            .ToLower()
            .Replace(" ", "-")
            .Replace("á", "a")
            .Replace("é", "e")
            .Replace("í", "i")
            .Replace("ó", "o")
            .Replace("ú", "u");

        var local = new Local
        {
            UsuarioId = usuario.Id,
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            Direccion = dto.Direccion,
            Coordenadas = dto.Coordenadas,
            LogoUrl = dto.LogoUrl,
            Slug = slug,
            Activo = false // espera verificación
        };

        _context.Locales.Add(local);
        await _context.SaveChangesAsync();

        return Ok("Solicitud de registro enviada. Te contactaremos pronto.");
    }


    // Incio de sesion
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var usuario = _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefault(u => u.Email == dto.Email && u.Activo);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            return Unauthorized("Credenciales inválidas");

        var token = JwtHelper.GenerarToken(usuario, _jwtSettings);
        return Ok(new { token });
    }


    // Inicio de sesion con la cuenta de GOOGLE
    [HttpPost("google")]
    public async Task<IActionResult> LoginConGoogle([FromBody] GoogleLoginDto dto)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken);

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == payload.Email);

            if (usuario == null)
            {
                usuario = new Usuario
                {
                    Email = payload.Email,
                    PasswordHash = "", // no necesita contraseña local
                    RolId = 3, // cliente por defecto
                    Activo = true,
                    FechaCreacion = DateTime.UtcNow
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                var cliente = new Cliente
                {
                    UsuarioId = usuario.Id,
                    Nombre = "",
                    Apellido = "",
                    Telefono = ""
                };
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                usuario = await _context.Usuarios.Include(u => u.Rol)
                    .FirstOrDefaultAsync(u => u.Email == payload.Email);
            }

            var token = JwtHelper.GenerarToken(usuario, _jwtSettings);
            return Ok(new { token });
        }
        catch
        {
            return Unauthorized("Token de Google inválido.");
        }
    }
}
