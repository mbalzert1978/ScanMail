using System.Text;
using FileHandler.Adapter;
using FileHandler.IO;
using Xunit;

namespace Tests {
    public class End2EndFileAdapterTests : IDisposable {
        private readonly string _testDirectory;
        private readonly FileAdapter _fileAdapter;

        public End2EndFileAdapterTests() {
            _testDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(_testDirectory);
            SystemFileWrapper systemFileWrapper = new();
            _fileAdapter = new FileAdapter(systemFileWrapper, systemFileWrapper);
        }

        public void Dispose() {
            if (Directory.Exists(_testDirectory)) {
                Directory.Delete(_testDirectory, true);
            }
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task ReadFile_WhenFileExists_ShouldReturnCorrectContent() {
            // Arrange
            var fileName = "testFile.txt";
            var filePath = Path.Combine(_testDirectory, fileName);
            var expectedContent = "Hello, World!";
            var uriPath = new Uri(filePath);

            File.WriteAllText(filePath, expectedContent);

            // Act
            var (success, content, errorMessage) = await _fileAdapter.ReadFile(uriPath);

            // Assert
            Assert.True(success);
            Assert.Equal(Encoding.UTF8.GetBytes(expectedContent), content);
            Assert.Null(errorMessage);
        }

        [Fact]
        public async Task WriteFile_WhenGivenBytes_ShouldWriteCorrectContent() {
            // Arrange
            var fileName = "test/testFile.txt";
            var filePath = Path.Combine(_testDirectory, fileName);
            var content = Encoding.UTF8.GetBytes("Hello, World!");
            var uriPath = new Uri(filePath);

            // Act
            var (success, _, errorMessage) = await _fileAdapter.WriteFile(uriPath, content);

            // Assert
            Assert.True(success);
            Assert.Null(errorMessage);
            Assert.Equal(content, File.ReadAllBytes(filePath));
        }
    }
}
