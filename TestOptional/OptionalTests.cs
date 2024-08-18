using System;
using Xunit;

public class OptionalTests
{
  [Fact]
  public void Of_Should_Create_Optional_With_Value()
  {
    // Arrange
    var value = "Hello";

    // Act
    var optional = Optional<string>.Of(value);

    // Assert
    Assert.True(optional.HasValue);
    Assert.Equal("Hello", optional.GetValueOrThrow());
  }

  [Fact]
  public void Of_Should_Throw_Exception_For_Null_Value()
  {
    // Arrange
    string value = null;

    // Act & Assert
    Assert.Throws<ArgumentNullException>(() => Optional<string>.Of(value));
  }

  [Fact]
  public void OfNullable_Should_Create_Optional_With_Value()
  {
    // Arrange
    string value = "Hello";

    // Act
    var optional = Optional<string>.OfNullable(value);

    // Assert
    Assert.True(optional.HasValue);
    Assert.Equal("Hello", optional.GetValueOrThrow());
  }

  [Fact]
  public void OfNullable_Should_Create_Empty_Optional_For_Null_Value()
  {
    // Arrange
    string value = null;

    // Act
    var optional = Optional<string>.OfNullable(value);

    // Assert
    Assert.False(optional.HasValue);
  }

  [Fact]
  public void Empty_Should_Create_Empty_Optional()
  {
    // Act
    var optional = Optional<string>.Empty();

    // Assert
    Assert.False(optional.HasValue);
  }

  [Fact]
  public void GetValueOrThrow_Should_Return_Value_If_Present()
  {
    // Arrange
    var optional = Optional<int>.Of(5);

    // Act
    var value = optional.GetValueOrThrow();

    // Assert
    Assert.Equal(5, value);
  }

  [Fact]
  public void GetValueOrThrow_Should_Throw_Exception_If_Empty()
  {
    // Arrange
    var optional = Optional<int>.Empty();

    // Act & Assert
    Assert.Throws<InvalidOperationException>(() => optional.GetValueOrThrow());
  }

  [Fact]
  public void OrElse_Should_Return_Value_If_Present()
  {
    // Arrange
    var optional = Optional<int>.Of(5);

    // Act
    var value = optional.OrElse(10);

    // Assert
    Assert.Equal(5, value);
  }

  [Fact]
  public void OrElse_Should_Return_Alternative_If_Empty()
  {
    // Arrange
    var optional = Optional<int>.Empty();

    // Act
    var value = optional.OrElse(10);

    // Assert
    Assert.Equal(10, value);
  }

  [Fact]
  public void OrElseGet_Should_Return_Value_If_Present()
  {
    // Arrange
    var optional = Optional<int>.Of(5);

    // Act
    var value = optional.OrElseGet(() => 10);

    // Assert
    Assert.Equal(5, value);
  }

  [Fact]
  public void OrElseGet_Should_Return_Alternative_From_Function_If_Empty()
  {
    // Arrange
    var optional = Optional<int>.Empty();

    // Act
    var value = optional.OrElseGet(() => 10);

    // Assert
    Assert.Equal(10, value);
  }

  [Fact]
  public void IfPresent_Should_Execute_Action_If_Value_Is_Present()
  {
    // Arrange
    var optional = Optional<int>.Of(5);
    var wasCalled = false;

    // Act
    optional.IfPresent(value => wasCalled = true);

    // Assert
    Assert.True(wasCalled);
  }

  [Fact]
  public void IfPresent_Should_Not_Execute_Action_If_Value_Is_Empty()
  {
    // Arrange
    var optional = Optional<int>.Empty();
    var wasCalled = false;

    // Act
    optional.IfPresent(value => wasCalled = true);

    // Assert
    Assert.False(wasCalled);
  }

  [Fact]
  public void Map_Should_Transform_Value_If_Present()
  {
    // Arrange
    var optional = Optional<string>.Of("Hello");

    // Act
    var lengthOptional = optional.Map(value => value.Length);

    // Assert
    Assert.True(lengthOptional.HasValue);
    Assert.Equal(5, lengthOptional.GetValueOrThrow());
  }

  [Fact]
  public void Map_Should_Return_Empty_Optional_If_Empty()
  {
    // Arrange
    var optional = Optional<string>.Empty();

    // Act
    var lengthOptional = optional.Map(value => value.Length);

    // Assert
    Assert.False(lengthOptional.HasValue);
  }

  [Fact]
  public void FlatMap_Should_Apply_Function_If_Value_Is_Present()
  {
    // Arrange
    var optional = Optional<string>.Of("Hello");

    // Act
    var result = optional.FlatMap(value =>
    {
      if (value.Length > 3)
        return Optional<string>.Of("World");
      else
        return Optional<string>.Empty();
    });

    // Assert
    Assert.True(result.HasValue);
    Assert.Equal("World", result.GetValueOrThrow());
  }

  [Fact]
  public void FlatMap_Should_Return_Empty_Optional_If_Empty()
  {
    // Arrange
    var optional = Optional<string>.Empty();

    // Act
    var result = optional.FlatMap(value =>
    {
      return Optional<string>.Of("World");
    });

    // Assert
    Assert.False(result.HasValue);
  }
}
