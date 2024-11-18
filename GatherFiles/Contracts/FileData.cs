using SharedKernel.Abstractions;

namespace GatherFiles.Contracts;

public record struct FileData(string Path, byte[] Content) : IFrom<(string path, byte[] content), FileData> {
    public static FileData From((string path, byte[] content) data) => new(data.path, data.content);
}
