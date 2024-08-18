public class Optional<T>
{
  private readonly T _value;

  private Optional(T value)
  {
    _value = value;
    HasValue = !EqualityComparer<T>.Default.Equals(value, default);
  }

  public bool HasValue { get; }

  public bool IsPresent()
  {
    return HasValue;
  }

  public T GetValueOrThrow()
  {
    if (!HasValue)
      throw new InvalidOperationException("No value present");
    return _value;
  }

  public T OrElse(T other)
  {
    return HasValue ? _value : other;
  }

  public T OrElseGet(Func<T> other)
  {
    return HasValue ? _value : other();
  }

  public void IfPresent(Action<T> action)
  {
    if (HasValue)
    {
      action(_value);
    }
  }

  // Método Map: Transforma o valor se ele estiver presente
  public Optional<TResult> Map<TResult>(Func<T, TResult> mapper)
  {
    if (!HasValue)
      return Optional<TResult>.Empty();
    return Optional<TResult>.OfNullable(mapper(_value));
  }

  // Método FlatMap (ou Bind): Similar ao Map, mas evita encapsulamento aninhado
  public Optional<TResult> FlatMap<TResult>(Func<T, Optional<TResult>> mapper)
  {
    if (!HasValue)
      return Optional<TResult>.Empty();
    return mapper(_value);
  }

  public static Optional<T> Of(T value)
  {
    if (value == null)
    {
      throw new ArgumentNullException(nameof(value), "Value cannot be null");
    }
    return new Optional<T>(value);
  }

  public static Optional<T> OfNullable(T value)
  {
    return new Optional<T>(value);
  }

  public static Optional<T> Empty()
  {
    return new Optional<T>(default(T));
  }

  public override string ToString()
  {
    return HasValue ? $"Optional[{_value}]" : "Optional.Empty";
  }
}