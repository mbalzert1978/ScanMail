using SM.Domain.Common;

namespace SM.UnitTests.Domain.Common;

public class EntityTests
{
    [Fact]
    public void EntityWhenComparedToAnotherEntityShouldReturnTrueWhenTheyHaveTheSameId()
    {
        // Given
        (var a, var b, _) = FetchIds(Guid.NewGuid());

        // When

        // Then
        Assert.Equal(a, b);
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact]
    public void EntityWhenComparedToAnotherEntityShouldReturnFalseWhenTheyAreNotTheSameType()
    {
        (var a, _, var c) = FetchIds();

        Assert.NotSame(a, c);
    }

    [Fact]
    public void EntitiesOfDifferentTypesWhenComparedUsingEqualsMethodReturnFalse()
    {
        (var a, _, var c) = FetchIds();

        Assert.False(a.Equals(c));
    }

    [Fact]
    public void EntitiesOfDifferentTypesWhenComparedUsingObjectReferenceEqualsReturnFalse()
    {
        (var a, _, var c) = FetchIds();

        Assert.False(ReferenceEquals(a, c));
    }

    [Fact]
    public void EntitiesOfDifferentTypesWhenComparedUsingObjectReferenceEqualsWithNullReturnFalse()
    {
        // Arrange
        FakeEntity<Guid> a = new(Guid.NewGuid());
        FakeEntity<Guid>? b = null;

        Assert.False(ReferenceEquals(a, b));
    }

    private class FakeEntity<TId>(TId id) : Entity<TId>(id) where TId : notnull { }

    private static (FakeEntity<Guid> a, FakeEntity<Guid> b, FakeEntity<int> c) FetchIds(Guid? guid = default) =>
         (new(guid ?? Guid.NewGuid()), new(guid ?? Guid.NewGuid()), new(42));
}
