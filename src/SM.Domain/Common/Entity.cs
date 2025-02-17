using System.Diagnostics.CodeAnalysis;

namespace SM.Domain.Common;

internal abstract class Entity<TId>(TId id) : IEquatable<Entity<TId>>
    where TId : notnull
{
    internal TId Id { get; private set; } = id;

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> other && Id.Equals(other.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return other is not null && Id.Equals(other.Id);
    }
}
