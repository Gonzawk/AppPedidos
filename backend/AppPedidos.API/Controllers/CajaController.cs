using AppPedidos.API.DTOs;
using AppPedidos.API.Services.Caja;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppPedidos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CajaController : ControllerBase
    {
        private readonly ICajaService _cajaService;

        public CajaController(ICajaService cajaService)
        {
            _cajaService = cajaService;
        }

        [HttpGet("{localId}/cajas")]
        public async Task<IActionResult> ObtenerCajas(int localId, [FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        {
            var cajas = await _cajaService.ObtenerCajasPorRangoFechaAsync(localId, desde, hasta);
            return Ok(cajas);
        }
    }
}
