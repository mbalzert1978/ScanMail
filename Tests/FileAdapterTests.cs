using System.Text;
using FileHandler.Adapter;

namespace Tests;

public class FileAdapterTests {
    private readonly FileIoTestApi _testApi = new();

    [Fact]
    public async Task ReadFile_WhenFileExists_ShouldReturnCorrectContent() {
        var filePath = new Uri("file:///test/file.txt");
        var expectedContent = Encoding.UTF8.GetBytes("Hello, World!");
        _testApi.AddFile(filePath.LocalPath, expectedContent);

        var (success, content, errorMessage) = await _testApi.ReadFile(filePath);

        Assert.True(success);
        Assert.Equal(expectedContent, content);
        Assert.Null(errorMessage);
    }

    [Fact]
    public async Task WriteFile_WhenNewFileCreated_ShouldWriteCorrectContent() {
        var filePath = new Uri("file:///test/newFile.txt");
        var expectedContent = Encoding.UTF8.GetBytes("New content");

        var (writeSuccess, _, writeErrorMessage) = await _testApi.WriteFile(
            filePath,
            expectedContent
        );
        var storedContent = _testApi.GetFileContents(filePath.LocalPath);

        Assert.True(writeSuccess);
        Assert.Null(writeErrorMessage);
        Assert.Equal(expectedContent, storedContent);
    }

    [Fact]
    public async Task ReadFile_WhenFileNotFound_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///nonexistent.txt");
        var (success, _, errorMessage) = await _testApi.ReadFile(filePath);

        Assert.False(success);
        Assert.Contains("File not found", errorMessage);
    }

    [Fact]
    public async Task ReadFile_WhenUnauthorizedAccess_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///test/unauthorized.txt");
        _testApi.SetExists(true);
        _testApi.SetReadException(new UnauthorizedAccessException("Access denied"));

        var (success, _, errorMessage) = await _testApi.ReadFile(filePath);

        Assert.False(success);
        Assert.Contains("Access denied", errorMessage);
    }

    [Fact]
    public async Task ReadFile_WhenPathTooLong_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///" + new string('a', 260));
        _testApi.SetExists(true);
        _testApi.SetReadException(new PathTooLongException("Path is too long"));

        var (success, _, errorMessage) = await _testApi.ReadFile(filePath);

        Assert.False(success);
        Assert.Contains("Path is too long", errorMessage);
    }

    [Fact]
    public async Task ReadFile_WhenDirectoryNotFound_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///nonexistent/dir/file.txt");
        _testApi.SetExists(true);
        _testApi.SetReadException(new DirectoryNotFoundException("Directory not found"));

        var (success, _, errorMessage) = await _testApi.ReadFile(filePath);

        Assert.False(success);
        Assert.Contains("Directory not found", errorMessage);
    }

    [Fact]
    public async Task ReadFile_WhenIOError_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///test/ioError.txt");
        _testApi.SetExists(true);
        _testApi.SetReadException(new IOException("I/O error"));

        var (success, _, errorMessage) = await _testApi.ReadFile(filePath);

        Assert.False(success);
        Assert.Contains("I/O error occurred", errorMessage);
    }

    [Fact]
    public async Task WriteFile_WhenUnauthorizedAccess_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///test/unauthorized.txt");
        var data = Encoding.UTF8.GetBytes("Test content");
        _testApi.SetWriteException(new UnauthorizedAccessException("Access denied"));

        var (success, _, errorMessage) = await _testApi.WriteFile(filePath, data);

        Assert.False(success);
        Assert.Contains("Access denied", errorMessage);
    }

    [Fact]
    public async Task WriteFile_WhenPathTooLong_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///" + new string('a', 260));
        var data = Encoding.UTF8.GetBytes("Test content");
        _testApi.SetWriteException(new PathTooLongException("Path too long"));

        var (success, _, errorMessage) = await _testApi.WriteFile(filePath, data);

        Assert.False(success);
        Assert.Contains("Path is too long", errorMessage);
    }

    [Fact]
    public async Task WriteFile_WhenDirectoryNotFound_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///nonexistent/dir/file.txt");
        var data = Encoding.UTF8.GetBytes("Test content");
        _testApi.SetWriteException(new DirectoryNotFoundException("Directory not found"));

        var (success, _, errorMessage) = await _testApi.WriteFile(filePath, data);

        Assert.False(success);
        Assert.Contains("Directory not found", errorMessage);
    }

    [Fact]
    public async Task WriteFile_WhenIOError_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///test/ioError.txt");
        var data = Encoding.UTF8.GetBytes("Test content");
        _testApi.SetWriteException(new IOException("I/O error"));

        var (success, _, errorMessage) = await _testApi.WriteFile(filePath, data);

        Assert.False(success);
        Assert.Contains("I/O error occurred", errorMessage);
    }

    [Fact]
    public async Task WriteFile_WhenFilePathIsNull_ShouldReturnErrorResult() {
        var filePath = null as Uri;
        var data = Encoding.UTF8.GetBytes("Test content");

        var (success, _, errorMessage) = await _testApi.WriteFile(filePath!, data);

        Assert.False(success);
        Assert.Contains("File path cannot be empty", errorMessage);
    }

    [Fact]
    public async Task WriteFile_WhenDataIsEmpty_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///test/file.txt");
        var data = Array.Empty<byte>();

        var (success, _, errorMessage) = await _testApi.WriteFile(filePath, data);

        Assert.False(success);
        Assert.Contains("Bytes cannot be empty: /test/file.txt", errorMessage);
    }

    [Fact]
    public async Task WriteFile_WhenDataIsNull_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///test/file.txt");
        byte[]? data = null;

        var (success, _, errorMessage) = await _testApi.WriteFile(filePath, data!);

        Assert.False(success);
        Assert.Contains("Bytes cannot be null: /test/file.txt", errorMessage);
    }

    [Fact]
    public async Task WriteFile_WhenGetDirectoryReturnsNull_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///test/file.txt");
        _testApi.SetWriterDirectoryOverride(string.Empty);

        byte[] data = Encoding.UTF8.GetBytes("Test content");
        var (success, _, errorMessage) = await _testApi.WriteFile(filePath, data);

        Assert.False(success);
        Assert.Contains("Invalid directory path: /test/file.txt", errorMessage);
    }

    [Fact]
    public async Task ReadFile_WhenFilePathIsNull_ShouldReturnErrorResult() {
        var filePath = null as Uri;

        var (success, _, errorMessage) = await _testApi.ReadFile(filePath!);

        Assert.False(success);
        Assert.Contains("File path cannot be empty", errorMessage);
    }

    [Fact]
    public async Task ReadFile_WhenUnexpectedException_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///test/file.txt");
        _testApi.SetExists(true);
        _testApi.SetReadException(new Exception("Unexpected error"));

        var (success, _, errorMessage) = await _testApi.ReadFile(filePath);

        Assert.False(success);
        Assert.Contains(
            "Unexpected error occurred while reading the file: /test/file.txt",
            errorMessage
        );
    }

    [Fact]
    public async Task WriteFile_WhenUnexpectedException_ShouldReturnErrorResult() {
        var filePath = new Uri("file:///test/file.txt");
        var data = Encoding.UTF8.GetBytes("Test content");
        _testApi.SetWriteException(new Exception("Unexpected error"));

        var (success, _, errorMessage) = await _testApi.WriteFile(filePath, data);

        Assert.False(success);
        Assert.Contains(
            "Unexpected error occurred while writing the file: /test/file.txt",
            errorMessage
        );
    }
}
