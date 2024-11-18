using SharedKernel.Abstractions;

namespace GatherFiles.Contracts;

public readonly record struct GatherError(string Message) : IFrom<string, GatherError> {
    public static GatherError From(string value) => new(value);
}
