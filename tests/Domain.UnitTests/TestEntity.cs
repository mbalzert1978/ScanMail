using Domain.Primitives;
using FluentAssertions;

namespace Domain.UnitTests;

public class EntityTests
{
    [Fact]
    public void Equality_WhenComparingDifferentEntityTypes_ShouldReturnFalse()
    {
        // Arrange
        var (entityA, entityB) = CreateDifferentEntities();

        // Assert
        entityA.Should().NotBe(entityB);
        (entityA == entityB).Should().BeFalse();
        (entityA != entityB).Should().BeTrue();
        entityA.Equals(entityB).Should().BeFalse();
        entityA.Equals((object)entityB).Should().BeFalse();
        ReferenceEquals(entityA, entityB).Should().BeFalse();
    }

    [Fact]
    public void Equality_WhenComparingEntityWithNull_ShouldReturnFalse()
    {
        // Arrange
        var entity = new FakeEntityA(new FakeID());

        // Assert
        entity.Should().NotBeNull();
        (entity == null).Should().BeFalse();
        (entity != null).Should().BeTrue();
        entity?.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void Equality_WhenComparingSameEntityType_ShouldReturnTrue()
    {
        // Arrange
        var id = new FakeID();
        var entity1 = new FakeEntityA(id);
        var entity2 = new FakeEntityA(id);

        // Assert
        entity1.Should().Be(entity2);
        (entity1 == entity2).Should().BeTrue();
        (entity1 != entity2).Should().BeFalse();
        entity1.Equals(entity2).Should().BeTrue();
        entity1.Equals((object)entity2).Should().BeTrue();
    }
    [Fact]
    public void GetHashCode_ShouldBeConsistent()
    {
        // Arrange
        var id = new FakeID();
        var entity1 = new FakeEntityA(id);
        var entity2 = new FakeEntityA(id);

        // Act & Assert
        entity1.GetHashCode().Should().Be(entity2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldBeDifferentForDifferentIds()
    {
        // Arrange
        var entity1 = new FakeEntityA(new FakeID());
        var entity2 = new FakeEntityA(new FakeID());

        // Act & Assert
        entity1.GetHashCode().Should().NotBe(entity2.GetHashCode());
    }

    [Fact]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        // Arrange
        var entity = new FakeEntityA(new FakeID());

        // Act & Assert
        entity.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void Equals_WithDifferentObjectType_ShouldReturnFalse()
    {
        // Arrange
        var entity = new FakeEntityA(new FakeID());
        var obj = new object();

        // Act & Assert
        entity.Equals(obj).Should().BeFalse();
    }

    [Fact]
    public void Equality_Operators_ShouldWorkCorrectly()
    {
        // Arrange
        var id = new FakeID();
        var entity1 = new FakeEntityA(id);
        var entity2 = new FakeEntityA(id);
        FakeEntityA? nullEntity = null;

        // Act & Assert
        (entity1 == entity2).Should().BeTrue();
        (entity1 != entity2).Should().BeFalse();
        (entity1 == nullEntity).Should().BeFalse();
        (nullEntity == entity1).Should().BeFalse();
    }

    private static (FakeEntityA, FakeEntityB) CreateDifferentEntities()
    {
        return (new FakeEntityA(new FakeID()), new FakeEntityB(new FakeID()));
    }

    private class FakeID : ValueObject
    {
        private readonly Guid _value = Guid.NewGuid();
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }
    }

    private class FakeEntityA(FakeID id) : Entity<FakeID>(id)
    {
    }

    private class FakeEntityB(FakeID id) : Entity<FakeID>(id)
    {
    }
}
