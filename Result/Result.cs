namespace Result;

public record struct Result<T>(bool Success, T Value, string? ErrorMessage)
    where T : notnull;
