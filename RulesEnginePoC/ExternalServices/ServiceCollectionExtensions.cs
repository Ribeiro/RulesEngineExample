using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExternalServices;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorkflowServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IWorkflowLoader>(_ =>
        {
            var workflowConfig = configuration.GetRequiredSection("Workflow");
            var workflowName = workflowConfig.GetValue<string>("Name");
            return new LocalFileWorkflowLoader(workflowName!);
        });

        return services;
    }
}