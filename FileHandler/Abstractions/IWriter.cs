namespace FileHandler.Abstractions;

public interface IWriter {
    string? GetDirectoryName(string? path);
    bool DirectoryExists(string? path);
    void CreateDirectory(string path);
    Task WriteAllBytesAsync(
        string path,
        byte[] bytes,
        CancellationToken cancellationToken = default
    );
}
