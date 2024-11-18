namespace GatherFiles.Contracts;

public record struct FileData(string Path, byte[] Content) {
    public static implicit operator (string Path, byte[] Content)(FileData value) =>
        (value.Path, value.Content);

    public static implicit operator FileData((string Path, byte[] Content) value) =>
        new(value.Path, value.Content);

    public readonly void Deconstruct(out string Path, out byte[] Content) =>
        (Path, Content) = (this.Path, this.Content);
}
