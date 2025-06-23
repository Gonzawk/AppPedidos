using AppPedidos.API.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppPedidos.API.Services.Caja
{
    public interface ICajaService
    {
        Task<CajaDiariaDto> CalcularCajaDiariaAsync(int localId, DateTime fecha);
        Task<List<CajaDiariaDto>> ObtenerCajasPorRangoFechaAsync(int localId, DateTime desde, DateTime hasta);
    }
}
