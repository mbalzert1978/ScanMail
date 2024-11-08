using Result;

namespace FileHandler.Abstractions;

public interface IFileReader {
    Task<Result<byte[]>> ReadFile(Uri filePath, CancellationToken cancellationToken);
}
