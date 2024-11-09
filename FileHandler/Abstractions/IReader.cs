namespace FileHandler.Abstractions;

public interface IReader {
    bool Exists(string path);
    Task<byte[]> ReadBytesAsync(string path, CancellationToken cancellationToken = default);
}
