namespace SharedKernel.Abstractions;

public interface IInto<out T> {
    T Into();
}
