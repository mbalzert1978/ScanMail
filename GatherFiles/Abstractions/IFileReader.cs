using GatherFiles.Contracts;
using RustyOptions;

namespace GatherFiles.Abstractions;

public interface IFileReader {
    Task<Result<ICollection<(string @Path, byte[] Content)>, GatherError>> ReadFilesAsync(
        Uri @Path,
        CancellationToken cancellationToken
    );
}
