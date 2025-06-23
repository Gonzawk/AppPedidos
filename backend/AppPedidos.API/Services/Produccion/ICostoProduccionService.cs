using AppPedidos.API.DTOs;

namespace AppPedidos.API.Services.Produccion
{
    public interface ICostoProduccionService
    {
        Task<CostoProduccionDto> CalcularCostoProduccionAsync(int produccionId);
        Task<decimal> ObtenerCostoProduccionDelDiaAsync(int localId, DateTime fecha);
    }
}
