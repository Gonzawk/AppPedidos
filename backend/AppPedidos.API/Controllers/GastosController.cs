using AppPedidos.API.DTOs;
using AppPedidos.API.Services.Gastos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppPedidos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GastosController : ControllerBase
    {
        private readonly IGastosService _gastosService;

        public GastosController(IGastosService gastosService)
        {
            _gastosService = gastosService;
        }

        [HttpPost("fijo")]
        public async Task<IActionResult> RegistrarGastoFijo(GastoFijoDto dto)
        {
            await _gastosService.RegistrarGastoFijoAsync(dto);
            return Ok();
        }

        [HttpPost("variable")]
        public async Task<IActionResult> RegistrarGastoVariable(GastoVariableDto dto)
        {
            await _gastosService.RegistrarGastoVariableAsync(dto);
            return Ok();
        }

        [HttpGet("{localId}/fijos")]
        public async Task<IActionResult> ObtenerFijos(int localId)
        {
            var gastos = await _gastosService.ObtenerGastosFijosAsync(localId);
            return Ok(gastos);
        }

        [HttpGet("{localId}/variables")]
        public async Task<IActionResult> ObtenerVariables(int localId, [FromQuery] DateTime fecha)
        {
            var gastos = await _gastosService.ObtenerGastosVariablesAsync(localId, fecha);
            return Ok(gastos);
        }
    }
}
