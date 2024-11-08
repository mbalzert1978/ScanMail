using FileHandler.Abstractions;

namespace FileHandler.Adapter.Mocks;

public class MockReader : IReader {
    private readonly Dictionary<string, byte[]> _fileSystem = [];
    private bool _exists = false;
    private Exception? _exception = null;

    public void AddFile(string path, byte[] content) {
        _fileSystem[path] = content;
        _exists = true;
    }

    public void SetException(Exception exception) => _exception = exception;

    public void SetExists(bool exists) => _exists = exists;

    public bool Exists(string path) => _exists;

    public Task<byte[]> ReadBytesAsync(string path, CancellationToken cancellationToken) {
        if (_exception is not null)
            throw _exception;

        return Task.FromResult(_fileSystem[path]);
    }
}
