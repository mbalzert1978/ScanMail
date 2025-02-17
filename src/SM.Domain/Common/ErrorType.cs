namespace SM.Domain.Common;

internal sealed record ErrorType(string Code, string Description)
{
    internal static ErrorType None => new(string.Empty, string.Empty);
}
