using SharedKernel.Abstractions;

namespace GatherFiles.Contracts;

public readonly record struct Unprocessed(ICollection<FileContent> Files) : IFrom<ICollection<FileContent>, Unprocessed> {
    public static Unprocessed From(ICollection<FileContent> value) => new(value);
}

