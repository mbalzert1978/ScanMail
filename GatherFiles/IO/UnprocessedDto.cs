using System.Collections.Immutable;
using GatherFiles.Contracts;
using SharedKernel.Abstractions;

namespace GatherFiles.IO;

public sealed record UnprocessedDto(ImmutableArray<FileContentDto> Files)
    : IFrom<Unprocessed, UnprocessedDto> {
    public static UnprocessedDto From(Unprocessed source) =>
        new(source.Files.Select(FileContentDto.From).ToImmutableArray());
}
