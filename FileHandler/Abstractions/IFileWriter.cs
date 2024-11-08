using Result;

namespace FileHandler.Abstractions;

public interface IFileWriter {
    Task<Result<Unit>> WriteFile(Uri filePath, byte[] data, CancellationToken cancellationToken);
}
