namespace MonoCloud.Core.Helpers;

public interface IOptional
{
  bool HasValue { get; }

  object? GetValue();
}
