using AppPedidos.API.Services.Produccion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppPedidos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CostoProduccionController : ControllerBase
    {
        private readonly ICostoProduccionService _costoProduccionService;

        public CostoProduccionController(ICostoProduccionService costoProduccionService)
        {
            _costoProduccionService = costoProduccionService;
        }

        [HttpGet("{produccionId}/costo")]
        public async Task<IActionResult> ObtenerCostoProduccion(int produccionId)
        {
            var dto = await _costoProduccionService.CalcularCostoProduccionAsync(produccionId);
            if (dto == null)
                return NotFound($"No se encontró un costo de producción para la producción ID: {produccionId}");

            return Ok(dto);
        }
    }
}
