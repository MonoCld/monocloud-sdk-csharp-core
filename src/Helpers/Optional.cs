using System;

namespace MonoCloud.SDK.Core.Helpers;

/// <summary>
///
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct Optional<T> : IOptional
{
  /// <summary>
  ///
  /// </summary>
  public readonly bool HasValue { get; }

  /// <summary>
  ///
  /// </summary>
  public T? Value { get; }

  /// <summary>
  ///
  /// </summary>
  /// <param name="value"></param>
  /// <param name="hasValue"></param>
  public Optional(T value, bool hasValue = true)
  {
    Value = value;
    HasValue = hasValue;
  }

  /// <summary>
  ///
  /// </summary>
  /// <returns></returns>
  public object? GetValue() => Value;

  /// <summary>
  ///
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public static implicit operator Optional<T>(T value) => new(value);

  /// <summary>
  ///
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object? obj)
  {
    if (obj is Optional<T> optional)
    {
      return Equals(optional);
    }

    return false;
  }

  /// <summary>
  ///
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => HashCode.Combine(Value, HasValue);

  private bool Equals(Optional<T> other)
  {
    if (HasValue && other.HasValue)
    {
      return Equals(Value, other.Value);
    }

    return HasValue == other.HasValue;
  }

  /// <summary>
  ///
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static bool operator ==(Optional<T> left, Optional<T> right) => left.Equals(right);

  /// <summary>
  ///
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static bool operator !=(Optional<T> left, Optional<T> right) => !(left == right);
}
