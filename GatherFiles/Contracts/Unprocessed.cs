using SharedKernel.Abstractions;

namespace GatherFiles.Contracts;

public readonly record struct Unprocessed(ICollection<FileContent> Files) : IFrom<IEnumerable<FileContent>, Unprocessed> {
    public static Unprocessed From(IEnumerable<FileContent> value) => new(value.ToArray());
}

