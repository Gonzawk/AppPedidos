using AppPedidos.API.DTOs;
using AppPedidos.API.Services.Ventas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppPedidos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentasService _ventasService;

        public VentasController(IVentasService ventasService)
        {
            _ventasService = ventasService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta(VentaDto dto)
        {
            await _ventasService.RegistrarVentaAsync(dto);
            return Ok();
        }

        [HttpGet("{localId}/ventas")]
        public async Task<IActionResult> ObtenerVentas(int localId, [FromQuery] DateTime fecha)
        {
            var ventas = await _ventasService.ObtenerVentasPorFechaAsync(localId, fecha);
            return Ok(ventas);
        }
    }
}
