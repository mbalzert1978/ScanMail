using Domain.Primitives;
using FluentAssertions;

namespace Domain.UnitTests;

public class ValueObjectTests
{
    [Fact]
    public void GetAtomicValues_NoPropertiesValueObject_ShouldReturnEmptyCollection()
    {
        // Arrange
        var valueObject = new NoPropertiesValueObject();

        // Act
        var atomicValues = valueObject.GetAtomicValues();

        // Assert
        atomicValues.Should().BeEmpty();
    }

    [Fact]
    public void GetAtomicValues_EmptyValueObject_ShouldReturnEmptyCollection()
    {
        // Arrange
        var valueObject = new EmptyValueObject();

        // Act
        var atomicValues = valueObject.GetAtomicValues();

        // Assert
        atomicValues.Should().BeEmpty();
    }

    [Fact]
    public void GetAtomicValues_SinglePropertyValueObject_ShouldReturnSingleValue()
    {
        // Arrange
        var valueObject = new SinglePropertyValueObject("test");

        // Act
        var atomicValues = valueObject.GetAtomicValues();

        // Assert
        atomicValues.Should().ContainSingle().Which.Should().Be("test");
    }

    [Fact]
    public void GetAtomicValues_MultiplePropertiesValueObject_ShouldReturnAllValues()
    {
        // Arrange
        var valueObject = new MultiplePropertiesValueObject("test", 123);

        // Act
        var atomicValues = valueObject.GetAtomicValues();

        // Assert
        atomicValues.Should().HaveCount(2).And.ContainInOrder("test", 123);
    }

    [Fact]
    public void Equals_TwoIdenticalValueObjects_ShouldBeEqual()
    {
        // Arrange
        var valueObject1 = new MultiplePropertiesValueObject("test", 123);
        var valueObject2 = new MultiplePropertiesValueObject("test", 123);

        // Act & Assert
        valueObject1.Should().Be(valueObject2);
        (valueObject1 == valueObject2).Should().BeTrue();
        (valueObject1 != valueObject2).Should().BeFalse();
    }

    [Fact]
    public void Equals_TwoDifferentValueObjects_ShouldNotBeEqual()
    {
        // Arrange
        var valueObject1 = new MultiplePropertiesValueObject("test", 123);
        var valueObject2 = new MultiplePropertiesValueObject("test", 456);

        // Act & Assert
        valueObject1.Should().NotBe(valueObject2);
        (valueObject1 == valueObject2).Should().BeFalse();
        (valueObject1 != valueObject2).Should().BeTrue();
    }

    private class NoPropertiesValueObject : ValueObject
    {
        public override IEnumerable<object> GetAtomicValues() => [];
    }

    private class EmptyValueObject : ValueObject
    {
        public override IEnumerable<object> GetAtomicValues() => [];
    }

    private class SinglePropertyValueObject(string property) : ValueObject
    {
        public string Property { get; } = property;

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Property;
        }
    }

    private class MultiplePropertiesValueObject(string property1, int property2) : ValueObject
    {
        public string Property1 { get; } = property1;
        public int Property2 { get; } = property2;

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Property1;
            yield return Property2;
        }
    }
}
