using GatherFiles.Contracts;
using SharedKernel.Abstractions;

namespace GatherFiles.IO;

public sealed record FileContentDto(Guid Id, string Path, byte[] Content)
    : IFrom<FileContent, FileContentDto> {
    public static FileContentDto From(FileContent files) =>
        new(Guid.CreateVersion7(), files.GetFilePath().ToString(), files.Content);
}
