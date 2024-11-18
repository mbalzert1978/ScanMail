using SharedKernel.Abstractions;

namespace GatherFiles.Contracts;

public readonly record struct FileContent(Uri FilePath, byte[] Content) : IFrom<FileData, FileContent> {
    public static FileContent From(FileData value) => new(new Uri(value.Path), value.Content);
    public Uri GetFilePath() => FilePath;

    public Stream GetStream() => new MemoryStream(Content);
}
