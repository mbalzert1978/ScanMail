namespace Domain.Primitives;

public abstract class Entity<TId>(TId id) : IEquatable<Entity<TId>>
    where TId : ValueObject
{
    protected TId Id { get; private init; } = id;

    public override bool Equals(object? obj) => obj switch
    {
        Entity<TId> entity => Equals(entity),
        _ => false,
    };

    public bool Equals(Entity<TId>? other) => other switch
    {
        Entity<TId> entity => Id.Equals(entity.Id),
        _ => false,
    };

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => (left, right) switch
    {
        (Entity<TId> leftEntity, Entity<TId> rightEntity) => leftEntity.Equals(rightEntity),
        _ => false,
    };

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}
