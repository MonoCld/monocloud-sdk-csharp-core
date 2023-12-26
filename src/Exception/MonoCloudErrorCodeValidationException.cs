using MonoCloud.SDK.Core.Models;
using System.Collections.Generic;

namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Error Code Validation Exception
/// </summary>
public class MonoCloudErrorCodeValidationException : MonoCloudRequestException
{
  /// <summary>
  /// Initializes the MonoCloudErrorCodeValidationException Class
  /// </summary>
  /// <param name="response">The problem details returned from the server.</param>
  public MonoCloudErrorCodeValidationException(ErrorCodeValidationProblemDetails response) : base(response)
  {
    Errors = response.Errors;
  }

  public IEnumerable<ErrorCodeValidationError> Errors { get; set; }
}
