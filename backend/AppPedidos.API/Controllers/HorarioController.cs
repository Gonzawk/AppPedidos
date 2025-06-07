using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AppPedidos.API.Data;
using AppPedidos.API.Models;
using AppPedidos.API.DTOs;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "local")]
public class HorarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public HorarioController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Crear([FromBody] HorarioCreateDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = _context.Locales.FirstOrDefault(l => l.UsuarioId == userId);

        if (local == null)
            return BadRequest("No tenés un local registrado.");

        var yaExiste = _context.Horarios.Any(h =>
            h.LocalId == local.Id && h.DiaSemana == dto.DiaSemana);

        if (yaExiste)
            return BadRequest("Ya configuraste un horario para ese día.");

        var horario = new Horario
        {
            LocalId = local.Id,
            DiaSemana = dto.DiaSemana,
            HoraApertura = dto.HoraApertura,
            HoraCierre = dto.HoraCierre
        };

        _context.Horarios.Add(horario);
        _context.SaveChanges();

        return Ok("Horario guardado correctamente.");
    }

    [HttpPut]
    public IActionResult ActualizarHorarios([FromBody] List<HorarioDto> dtos)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = _context.Locales.FirstOrDefault(l => l.UsuarioId == userId);

        if (local == null)
            return BadRequest("No tenés un local registrado.");

        foreach (var dto in dtos)
        {
            var horario = _context.Horarios.FirstOrDefault(h => h.Id == dto.Id && h.LocalId == local.Id);

            if (horario != null)
            {
                horario.HoraApertura = dto.HoraApertura;
                horario.HoraCierre = dto.HoraCierre;
            }
            else if (dto.Id == 0)
            {
                _context.Horarios.Add(new Horario
                {
                    LocalId = local.Id,
                    DiaSemana = dto.DiaSemana,
                    HoraApertura = dto.HoraApertura,
                    HoraCierre = dto.HoraCierre
                });
            }
        }

        _context.SaveChanges();
        return Ok("Horarios actualizados.");
    }


    [HttpGet("mios")]
    public IActionResult ObtenerHorarios()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = _context.Locales.FirstOrDefault(l => l.UsuarioId == userId);

        if (local == null)
            return BadRequest("No tenés un local registrado.");

        var horarios = _context.Horarios
            .Where(h => h.LocalId == local.Id)
            .OrderBy(h => h.DiaSemana)
            .Select(h => new HorarioDto
            {
                Id = h.Id,
                DiaSemana = h.DiaSemana,
                HoraApertura = h.HoraApertura,
                HoraCierre = h.HoraCierre
            })
            .ToList();

        return Ok(horarios);
    }

}
