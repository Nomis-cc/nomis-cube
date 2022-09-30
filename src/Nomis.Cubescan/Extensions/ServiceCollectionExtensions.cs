using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Blockchain.Abstractions.Settings;
using Nomis.Cubescan.Interfaces;
using Nomis.Cubescan.Interfaces.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.Cubescan.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Cubescan service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddCubescanService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<CubescanSettings>(configuration);
            var settings = configuration.GetSettings<ApiVisibilitySettings>();
            if (settings.CubeAPIEnabled)
            {
                return services
                    .AddTransient<ICubescanClient, CubescanClient>()
                    .AddTransientInfrastructureService<ICubescanService, CubescanService>();
            }
            else
            {
                return services;
            }
        }
    }
}