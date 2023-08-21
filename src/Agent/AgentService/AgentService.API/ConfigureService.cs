using AgentService.Application.Plugins;
using Shared.Presentation.Middlewares;

namespace AgentService.API;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        Shared.Presentation.ConfigureServices.AddSharedPresentationServices(services, configuration);

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        foreach (var plugin in PluginCollection.GetPlugins)
        {
            plugin.AddPluginService(services, configuration);
        }

        return services;
    }

    public static void AddPipelines(this WebApplication app)
    {
        //if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<UnhandledExceptionMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        foreach (var plugin in PluginCollection.GetPlugins)
        {
            plugin.AddEndpoints(app);
        }
    }
}