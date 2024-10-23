namespace Domain.Primitives;

public abstract class ValueObject
{
    public abstract IEnumerable<object> GetAtomicValues();

    public bool Equals(ValueObject? other) => other switch
    {
        ValueObject vo => ValuesAreEqual(vo),
        _ => false,
    };

    public override bool Equals(object? other) => other switch
    {
        ValueObject vo => ValuesAreEqual(vo),
        _ => false,
    };

    public static bool operator ==(ValueObject? left, ValueObject? right) => (left, right) switch
    {
        (ValueObject leftValue, ValueObject rightValue) => leftValue.ValuesAreEqual(rightValue),
        _ => false,
    };

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }

    public override int GetHashCode() =>
        GetAtomicValues().Aggregate(default(int), HashCode.Combine);

    private bool ValuesAreEqual(ValueObject other) =>
        GetAtomicValues().SequenceEqual(other.GetAtomicValues());
}
