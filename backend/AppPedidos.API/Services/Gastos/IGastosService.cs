using AppPedidos.API.DTOs;
using AppPedidos.API.Models;

namespace AppPedidos.API.Services.Gastos
{
    public interface IGastosService
    {
        Task RegistrarGastoFijoAsync(GastoFijoDto dto);
        Task RegistrarGastoVariableAsync(GastoVariableDto dto);
        Task<List<GastoFijo>> ObtenerGastosFijosAsync(int localId);
        Task<List<GastoVariable>> ObtenerGastosVariablesAsync(int localId, DateTime fecha);
    }

}
