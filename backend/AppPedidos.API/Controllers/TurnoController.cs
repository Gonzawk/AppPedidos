using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AppPedidos.API.Data;
using AppPedidos.API.Models;
using AppPedidos.API.DTOs;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "local")]
public class TurnoController : ControllerBase
{
    private readonly AppDbContext _context;

    public TurnoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("mios")]
    public IActionResult ObtenerTurnos()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = _context.Locales.FirstOrDefault(l => l.UsuarioId == userId);

        if (local == null)
            return BadRequest("No tenés un local registrado.");

        var turnos = _context.Turnos
            .Where(t => t.LocalId == local.Id)
            .OrderBy(t => t.DiaSemana)
            .ThenBy(t => t.NumeroTurno)
            .Select(t => new TurnoDto
            {
                Id = t.Id,
                DiaSemana = t.DiaSemana,
                NumeroTurno = t.NumeroTurno,
                HoraApertura = t.HoraApertura,
                HoraCierre = t.HoraCierre
            })
            .ToList();

        return Ok(turnos);
    }

    [HttpPost]
    public IActionResult CrearTurno([FromBody] TurnoCreateDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = _context.Locales.FirstOrDefault(l => l.UsuarioId == userId);

        if (local == null)
            return BadRequest("No tenés un local registrado.");

        var existe = _context.Turnos.Any(t =>
            t.LocalId == local.Id &&
            t.DiaSemana == dto.DiaSemana &&
            t.NumeroTurno == dto.NumeroTurno);

        if (existe)
            return BadRequest("Ya existe un turno para ese día y número.");

        var turno = new Turno
        {
            LocalId = local.Id,
            DiaSemana = dto.DiaSemana,
            NumeroTurno = dto.NumeroTurno,
            HoraApertura = dto.HoraApertura,
            HoraCierre = dto.HoraCierre
        };

        _context.Turnos.Add(turno);
        _context.SaveChanges();

        return Ok("Turno creado correctamente.");
    }

    [HttpPut]
    public IActionResult ActualizarTurnos([FromBody] List<TurnoDto> dtos)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var local = _context.Locales.FirstOrDefault(l => l.UsuarioId == userId);

        if (local == null)
            return BadRequest("No tenés un local registrado.");

        foreach (var dto in dtos)
        {
            if (!EsHorarioValido(dto.HoraApertura, dto.HoraCierre))
                continue;

            var existente = _context.Turnos.FirstOrDefault(t =>
                t.LocalId == local.Id &&
                t.DiaSemana == dto.DiaSemana &&
                t.NumeroTurno == dto.NumeroTurno);

            if (existente != null)
            {
                existente.HoraApertura = dto.HoraApertura;
                existente.HoraCierre = dto.HoraCierre;
                existente.Activo = true;
            }
            else
            {
                _context.Turnos.Add(new Turno
                {
                    LocalId = local.Id,
                    DiaSemana = dto.DiaSemana,
                    NumeroTurno = dto.NumeroTurno,
                    HoraApertura = dto.HoraApertura,
                    HoraCierre = dto.HoraCierre,
                    Activo = true
                });
            }
        }


        _context.SaveChanges();
        return Ok("Turnos actualizados correctamente.");
    }

    private bool EsHorarioValido(TimeSpan apertura, TimeSpan cierre)
    {
        return apertura != cierre;
    }


}
