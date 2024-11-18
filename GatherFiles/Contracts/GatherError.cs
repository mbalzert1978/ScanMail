using SharedKernel.Abstractions;

namespace GatherFiles.Contracts;

public readonly record struct GatherError(string Message) : IFrom<string, GatherError>, IFrom<Exception, GatherError> {
    public static GatherError From(string value) => new(value);
    public static GatherError From(Exception value) => new(value.Message);
}
