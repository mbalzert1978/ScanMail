using Entry.IO.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

const string ConfigFile = "appsettings.json";

ConfigProvider.ConfigureLogging(
    new ConfigurationBuilder()
        .AddJsonFile(ConfigFile, optional: false, reloadOnChange: false)
        .Build(),
    new ServiceCollection()
);
