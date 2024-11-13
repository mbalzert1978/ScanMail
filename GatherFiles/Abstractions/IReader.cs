namespace GatherFiles.Abstractions;

public interface IReader {
    IEnumerable<string> EnumerateFiles(string path);
    bool DirectoryExists(string? path);
    Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = default);
}
