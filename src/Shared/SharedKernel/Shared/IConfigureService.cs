using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.Shared;

public interface IConfigureService
{
    void AddServices(IServiceCollection services);
}