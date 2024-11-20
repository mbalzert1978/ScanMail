using SharedKernel.Abstractions;

namespace GatherFiles.IO;

public record SerializableFileContent(string Path, string Content)
    : IFrom<FileContentDto, SerializableFileContent> {
    public static SerializableFileContent From(FileContentDto source) =>
        new(source.Path, Convert.ToBase64String(source.Content));
}
