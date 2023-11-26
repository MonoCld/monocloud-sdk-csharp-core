namespace MonoCloud.SDK.Core.Helpers;

/// <summary>
/// The IOptional Class
/// </summary>
public interface IOptional
{
  /// <summary>
  /// Specifies whether or not the object has a value
  /// </summary>
  bool HasValue { get; }

  /// <summary>
  /// Returns the value of the object
  /// </summary>
  /// <returns></returns>
  object? GetValue();
}
