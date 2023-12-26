namespace MonoCloud.SDK.Core.Models;

public class ErrorCodeValidationError
{
  /// <summary>
  /// The error code.
  /// </summary>
  public string Code { get; set; } = string.Empty;

  /// <summary>
  /// Brief explanation of the error.
  /// </summary>
  public string Description { get; set; } = string.Empty;
}
