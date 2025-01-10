using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Entry.IO.Configuration;

public static class ConfigProvider
{
    const string LogSection = "Logging";

    public static ServiceCollection ConfigureLogging(
        IConfiguration configuration,
        ServiceCollection services
    )
    {
        services
            .AddSingleton(configuration)
            .AddLogging(lb =>
                lb.AddConfiguration(configuration.GetSection(LogSection)).AddConsole()
            );

        return services;
    }
}
