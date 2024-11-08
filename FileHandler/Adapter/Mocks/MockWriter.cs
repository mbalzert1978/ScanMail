using FileHandler.Abstractions;

namespace FileHandler.Adapter.Mocks;

public class MockWriter : IWriter {
    private readonly Dictionary<string, byte[]> _fileSystem = [];
    private Exception? _exception = null;
    private string? _getDirectoryOverride = null;

    public void SetException(Exception exception) => _exception = exception;
    public void SetGetDirectoryNameOverride(string? path) => _getDirectoryOverride = path;

    public Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken) {
        if (_exception is not null)
            throw _exception;

        _fileSystem[path] = bytes;
        return Task.CompletedTask;
    }
    public string? GetDirectoryName(string? path) {
        if (_getDirectoryOverride is not null)
            path = _getDirectoryOverride;

        if (string.IsNullOrEmpty(path))
            return null;

        int lastSlashIndex = path.LastIndexOf('/');

        return path[..lastSlashIndex];
    }

    public byte[]? GetFileContents(string path) => _fileSystem.TryGetValue(path, out var content) ? content : null;

    public bool DirectoryExists(string? path) => !string.IsNullOrEmpty(path) && _fileSystem.Keys.Any(k => k.StartsWith(path));

    public void CreateDirectory(string path) { } // No-op in this mock implementation
}
