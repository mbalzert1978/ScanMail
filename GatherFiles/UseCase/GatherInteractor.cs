using GatherFiles.Abstractions;
using GatherFiles.Contracts;
using RustyOptions;
using RustyOptions.Async;

namespace GatherFiles.UseCase;

public class GatherInteractor(IFileReader reader) : IInteractor {
    public async Task<Result<Unprocessed, GatherError>> Handle(
        GatherRequestFrom request,
        CancellationToken cancellationToken = default
    ) =>
        await reader
            .ReadFilesAsync(request.Source, cancellationToken)
            .MapAsync(files => files.Select(FileContent.From))
            .MapAsync(Unprocessed.From);
}
