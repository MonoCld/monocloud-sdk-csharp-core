using System.Collections.Generic;

namespace MonoCloud.SDK.Core.Models;

public class ValidationProblemDetails : ProblemDetails
{
  /// <summary>
  /// A collection of validation errors
  /// </summary>
  public IEnumerable<ValidationError> Errors { get; set; } = new List<ValidationError>();
}
