using AppPedidos.API.Data;
using AppPedidos.API.DTOs;
using AppPedidos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppPedidos.API.Controllers
{
    [ApiController]
    [Route("api/public/catalogo")]
    public class CatalogoController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CatalogoController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<CatalogoDTO>>> ObtenerCatalogo()
        {
            var ahora = DateTime.UtcNow; // Si manejas timezone local, deberías convertir aquí
            var diaSemana = (int)ahora.DayOfWeek;
            var horaActual = ahora.TimeOfDay;

            var localesDisponibles = await _dbContext.Locales
                .Where(l => l.Activo)
                .Where(l => l.Turnos.Any(t =>
                    t.Activo &&
                    t.DiaSemana == diaSemana &&
                    t.HoraApertura <= horaActual &&
                    t.HoraCierre >= horaActual
                ))
                .Select(l => new CatalogoDTO
                {
                    LocalId = l.Id,
                    Nombre = l.Nombre,
                    Slug = l.Slug,
                    Telefono = l.Telefono,
                    Direccion = l.Direccion,
                    Coordenadas = l.Coordenadas,
                    LogoUrl = l.LogoUrl,
                    TurnosDisponibles = l.Turnos
                        .Where(t => t.Activo && t.DiaSemana == diaSemana)
                        .Select(t => new TurnoCatalogoDTO
                        {
                            NumeroTurno = t.NumeroTurno,
                            HoraApertura = t.HoraApertura,
                            HoraCierre = t.HoraCierre
                        }).ToList(),
                    Productos = l.Categorias
                        .SelectMany(c => c.Productos)
                        .Where(p => p.Activo && p.Stock > 0)
                        .Select(p => new ProductoCatalogoDTO
                        {
                            ProductoId = p.Id,
                            Nombre = p.Nombre,
                            Descripcion = p.Descripcion,
                            Precio = p.Precio,
                            ImagenUrl = p.ImagenUrl,
                            Modificadores = p.ProductoModificadores
                                .Select(pm => new ModificadorCatalogoDTO
                                {
                                    ModificadorId = pm.Modificador.Id,
                                    Nombre = pm.Modificador.Nombre,
                                    PrecioExtra = pm.Modificador.PrecioExtra
                                }).ToList()
                        }).ToList()
                })
                .ToListAsync();

            return Ok(localesDisponibles);
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<CatalogoDTO>> ObtenerCatalogoPorSlug(string slug)
        {
            var ahora = DateTime.UtcNow;
            var diaSemana = (int)ahora.DayOfWeek;
            var horaActual = ahora.TimeOfDay;

            var local = await _dbContext.Locales
                .Where(l => l.Activo && l.Slug == slug)
                .Where(l => l.Turnos.Any(t =>
                    t.Activo &&
                    t.DiaSemana == diaSemana &&
                    t.HoraApertura <= horaActual &&
                    t.HoraCierre >= horaActual
                ))
                .Select(l => new CatalogoDTO
                {
                    LocalId = l.Id,
                    Nombre = l.Nombre,
                    Slug = l.Slug,
                    Telefono = l.Telefono,
                    Direccion = l.Direccion,
                    Coordenadas = l.Coordenadas,
                    LogoUrl = l.LogoUrl,
                    TurnosDisponibles = l.Turnos
                        .Where(t => t.Activo && t.DiaSemana == diaSemana)
                        .Select(t => new TurnoCatalogoDTO
                        {
                            NumeroTurno = t.NumeroTurno,
                            HoraApertura = t.HoraApertura,
                            HoraCierre = t.HoraCierre
                        }).ToList(),
                    Productos = l.Categorias
                        .SelectMany(c => c.Productos)
                        .Where(p => p.Activo && p.Stock > 0)
                        .Select(p => new ProductoCatalogoDTO
                        {
                            ProductoId = p.Id,
                            Nombre = p.Nombre,
                            Descripcion = p.Descripcion,
                            Precio = p.Precio,
                            ImagenUrl = p.ImagenUrl,
                            Modificadores = p.ProductoModificadores
                                .Select(pm => new ModificadorCatalogoDTO
                                {
                                    ModificadorId = pm.Modificador.Id,
                                    Nombre = pm.Modificador.Nombre,
                                    PrecioExtra = pm.Modificador.PrecioExtra
                                }).ToList()
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (local == null) return NotFound();

            return Ok(local);
        }

    }
}
