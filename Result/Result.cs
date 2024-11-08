namespace Result;

public record struct Result<T>(bool Success, T Value, string? ErrorMessage)
    where T : notnull;

public static class Factories {
    public static Result<T> Ok<T>(T value)
        where T : notnull => new(true, value, null);

    public static Result<Unit> Ok() => new(true, new(), null);

    public static Result<T> Err<T>(string errorMessage)
        where T : notnull => new(false, default!, errorMessage);

    public static Result<T> Err<T>(string format, params object[] args)
        where T : notnull => new(false, default!, string.Format(format, args));
}
