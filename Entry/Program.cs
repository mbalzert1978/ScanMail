using GatherFiles.Abstractions;
using GatherFiles.Adapter;
using GatherFiles.IO;
using GatherFiles.UseCase;

var builder = Host.CreateDefaultBuilder(args);
var host = builder
    .ConfigureAppConfiguration(
        (hostingContext, config) => {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }
    )
    .ConfigureServices(
        (hostContext, services) => {
            // services.AddScoped<IUnprocessedRepository, UnprocessedRepository>(); // TODO: Implement EF Core repository.
            services.AddScoped<IReader, SystemFileWrapper>();
            services.AddScoped<IFileReader, FileAdapter>();
            services.AddScoped<IInteractor, GatherInteractor>();
            services.AddScoped<GatherController>();
        }
    )
    .ConfigureLogging(logging => {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;
var logger = services.GetRequiredService<ILogger<Program>>();
var configuration = services.GetRequiredService<IConfiguration>();
var watchDirectory = configuration.GetValue<string>("WatchDirectory");
var controller = services.GetRequiredService<GatherController>();
var cts = SetupCancellation();

static CancellationTokenSource SetupCancellation() {
    var cts = new CancellationTokenSource();
    Console.CancelKeyPress += (s, e) => {
        e.Cancel = true;
        cts.Cancel();
    };
    return cts;
}

await Run(logger, watchDirectory, controller, cts);

static async Task Run(
    ILogger<Program> logger,
    string? watchDirectory,
    GatherController controller,
    CancellationTokenSource cts
) {
    try {
        await RunWatchLoop(controller, watchDirectory!, cts.Token, logger);
    } catch (Exception ex) {
        logger.LogError(ex, "An error occurred: {Message}", ex.Message);
    }
}
static async Task RunWatchLoop(
    GatherController controller,
    string watchDirectory,
    CancellationToken cancellationToken,
    ILogger logger
) {
    ArgumentNullException.ThrowIfNull(controller);
    ArgumentException.ThrowIfNullOrEmpty(watchDirectory);

    while (!cancellationToken.IsCancellationRequested) {
        var request = new GatherRequestFrom(new Uri(watchDirectory));
        var result = await controller.GatherFilesAsync(request, cancellationToken);
        result.Match(
            unprocessed =>
                logger.LogInformation("Gathered {Count} files.", unprocessed.Files.Count),
            error => logger.LogError("Error: {Error}", error)
        );
        await Task.Delay(TimeSpan.FromSeconds(60), cancellationToken);
    }
}
