using GatherFiles.Abstractions;

namespace GatherFiles.IO;

public class SystemFileWrapper : IReader {
    public bool DirectoryExists(string? path) => Directory.Exists(path);

    public IEnumerable<string> EnumerateFiles(string path) => Directory.EnumerateFiles(path);

    public async Task<byte[]> ReadAllBytesAsync(
        string path,
        CancellationToken cancellationToken = default
    ) => await File.ReadAllBytesAsync(path, cancellationToken);
}
