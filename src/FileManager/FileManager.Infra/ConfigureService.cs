using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Infra;

public static class ConfigureServices
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        Shared.Infra.ConfigureServices.AddSharedInfraServices(services, configuration);

        return services;
    }

}