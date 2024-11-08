using FileHandler.Abstractions;

namespace FileHandler.IO;

public class SystemFileWrapper : IReader, IWriter {
    public void CreateDirectory(string path) => Directory.CreateDirectory(path);

    public bool DirectoryExists(string? path) => Directory.Exists(path);

    public bool Exists(string path) => File.Exists(path);

    public string? GetDirectoryName(string? path) => Path.GetDirectoryName(path);

    public async Task<byte[]> ReadBytesAsync(
        string path,
        CancellationToken cancellationToken = default
    ) => await File.ReadAllBytesAsync(path, cancellationToken);

    public async Task WriteAllBytesAsync(
        string path,
        byte[] bytes,
        CancellationToken cancellationToken = default
    ) => await File.WriteAllBytesAsync(path, bytes, cancellationToken);
}
