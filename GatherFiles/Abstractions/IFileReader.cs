using GatherFiles.Contracts;
using RustyOptions;

namespace GatherFiles.Abstractions;

public interface IFileReader {
    Task<Result<ICollection<FileData>, GatherError>> ReadFilesAsync(
        Uri @Path,
        CancellationToken cancellationToken
    );
}
