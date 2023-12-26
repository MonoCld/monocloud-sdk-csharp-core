using System.Collections.Generic;

namespace MonoCloud.SDK.Core.Models;

public class ErrorCodeValidationProblemDetails : ProblemDetails
{
  /// <summary>
  /// A collection of error codes
  /// </summary>
  public IEnumerable<ErrorCodeValidationError> Errors { get; set; } = new List<ErrorCodeValidationError>();
}
