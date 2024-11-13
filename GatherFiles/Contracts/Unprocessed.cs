namespace GatherFiles.Contracts;

public readonly record struct Unprocessed(ICollection<FileContent> Files);
