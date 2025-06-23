using AppPedidos.API.Services.Gastos;
using AppPedidos.API.Services.Ventas;
using AppPedidos.API.Services.Caja;
using Microsoft.Extensions.DependencyInjection;

namespace AppPedidos.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            // Finanzas
            services.AddScoped<ICajaService, CajaService>();
            services.AddScoped<IVentasService, VentasService>();
            services.AddScoped<IGastosService, GastosService>();

            // Aquí se pueden agregar más capas organizadas por dominio

            return services;
        }
    }
}

