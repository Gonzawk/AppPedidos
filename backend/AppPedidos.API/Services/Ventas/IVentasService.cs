using AppPedidos.API.DTOs;
using AppPedidos.API.Models;

namespace AppPedidos.API.Services.Ventas
{
    public interface IVentasService
    {
        Task<int> RegistrarVentaAsync(VentaDto dto);
        Task<List<Venta>> ObtenerVentasPorFechaAsync(int localId, DateTime fecha);
    }

}
