using System;

namespace MonoCloud.Core.Helpers;

public readonly struct Optional<T> : IOptional
{
  public readonly bool HasValue { get; }

  public T? Value { get; }

  public Optional(T value, bool hasValue = true)
  {
    Value = value;
    HasValue = hasValue;
  }

  public object? GetValue() => Value;

  public static implicit operator Optional<T>(T value) => new(value);

  public override bool Equals(object? obj)
  {
    if (obj is Optional<T> optional)
    {
      return Equals(optional);
    }

    return false;
  }

  public override int GetHashCode() => HashCode.Combine(Value, HasValue);

  private bool Equals(Optional<T> other)
  {
    if (HasValue && other.HasValue)
    {
      return Equals(Value, other.Value);
    }

    return HasValue == other.HasValue;
  }

  public static bool operator ==(Optional<T> left, Optional<T> right) => left.Equals(right);

  public static bool operator !=(Optional<T> left, Optional<T> right) => !(left == right);
}
