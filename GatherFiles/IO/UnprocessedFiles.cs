using System.Text.Json;
using SharedKernel.Abstractions;

namespace GatherFiles.IO;

public sealed record UnprocessedFiles(
    Guid Id,
    string Files,
    bool? IsRead,
    DateTime? CreatedAt,
    DateTime? ProcessedAt
) : IFrom<UnprocessedDto, UnprocessedFiles> {
    public static UnprocessedFiles From(UnprocessedDto source) =>
        new(
            Guid.CreateVersion7(),
            JsonSerializer.Serialize(source.Files.Select(SerializableFileContent.From)),
            false,
            null,
            null
        );
}
