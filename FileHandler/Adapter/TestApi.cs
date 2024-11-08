using FileHandler.Adapter.Mocks;
using Result;

namespace FileHandler.Adapter;

public class FileIoTestApi {
    private readonly MockReader _mockReader;
    private readonly MockWriter _mockWriter;
    private readonly FileAdapter _fileAdapter;

    public FileIoTestApi() {
        _mockReader = new MockReader();
        _mockWriter = new MockWriter();
        _fileAdapter = new FileAdapter(_mockReader, _mockWriter);
    }

    public void AddFile(string path, byte[] content) => _mockReader.AddFile(path, content);

    public void SetExists(bool exists) => _mockReader.SetExists(exists);

    public void SetReadException(Exception exception) => _mockReader.SetException(exception);

    public void SetWriteException(Exception exception) => _mockWriter.SetException(exception);
    public void SetWriterDirectoryOverride(string? path) => _mockWriter.SetGetDirectoryNameOverride(path);

    public async Task<Result<byte[]>> ReadFile(
        Uri filePath,
        CancellationToken cancellationToken = default
    ) => await _fileAdapter.ReadFile(filePath, cancellationToken);

    public async Task<Result<Unit>> WriteFile(
        Uri filePath,
        byte[] data,
        CancellationToken cancellationToken = default
    ) => await _fileAdapter.WriteFile(filePath, data, cancellationToken);

    public byte[]? GetFileContents(string path) => _mockWriter.GetFileContents(path);
}
