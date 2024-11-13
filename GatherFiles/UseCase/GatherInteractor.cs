using GatherFiles.Abstractions;
using GatherFiles.Contracts;
using RustyOptions;
using RustyOptions.Async;

namespace GatherFiles.UseCase;

public class GatherInteractor(IFileReader reader) : IInteractor {
    public async Task<Result<Unprocessed, GatherError>> GatherFilesAsync(
        GatherRequestFrom request,
        CancellationToken cancellationToken = default
    ) =>
        await reader
            .ReadFilesAsync(request.Source, cancellationToken)
            .MapAsync(files =>
                files.Select(file => new FileContent(new Uri(file.Path), file.Content))
            )
            .MapAsync(unprocessed => new Unprocessed(unprocessed.ToArray()));
}
