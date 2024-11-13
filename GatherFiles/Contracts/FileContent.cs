namespace GatherFiles.Contracts;

public readonly record struct FileContent(Uri FilePath, byte[] Content)
{
    public Uri GetFilePath() => FilePath;
    public Stream GetStream() => new MemoryStream(Content);
}
