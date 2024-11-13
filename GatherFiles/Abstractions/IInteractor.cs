using GatherFiles.Contracts;
using GatherFiles.UseCase;
using RustyOptions;

namespace GatherFiles.Abstractions;

public interface IInteractor {
    public Task<Result<Unprocessed, GatherError>> GatherFilesAsync(
        GatherRequestFrom request,
        CancellationToken cancellationToken = default
    );
}
