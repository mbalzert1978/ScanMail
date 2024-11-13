using GatherFiles.Abstractions;

namespace GatherFiles.Contracts;

public readonly record struct GatherError(string Message) : IFromString<GatherError> {
    public static GatherError FromString(string value) => new(value);

    public static GatherError FromString(string value, params object[] args) =>
        new(string.Format(value, args));
}
