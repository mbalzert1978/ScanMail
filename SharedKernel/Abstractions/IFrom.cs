namespace SharedKernel.Abstractions;

public interface IFrom<in T, out TSelf>
    where T : notnull
    where TSelf : IFrom<T, TSelf> {
    static abstract TSelf From(T value);
}
