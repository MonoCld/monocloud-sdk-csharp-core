using MonoCloud.SDK.Core.Models;

namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Exception
/// </summary>
public class MonoCloudException : System.Exception
{
  /// <summary>
  /// Initializes the MonoCloudException Class
  /// </summary>
  /// <param name="message">The message returned from the server.</param>
  public MonoCloudException(string message) : base(message)
  {
  }

  /// <summary>
  /// Converts the Problem Details returned from the server into an exception
  /// </summary>
  /// <param name="problemDetails">The problem details returned from the server.</param>
  /// <returns></returns>
  public static MonoCloudException ThrowErr(ProblemDetails problemDetails) =>
    throw (problemDetails.Status switch
    {
      400 => new MonoCloudBadRequestException(problemDetails),
      401 => new MonoCloudUnauthorizedException(problemDetails),
      403 => new MonoCloudForbiddenException(problemDetails),
      404 => new MonoCloudNotFoundException(problemDetails),
      409 => new MonoCloudConflictException(problemDetails),
      422 when problemDetails is ErrorCodeValidationProblemDetails v => new MonoCloudErrorCodeValidationException(v),
      422 when problemDetails is KeyValidationProblemDetails v => new MonoCloudKeyValidationException(v),
      429 => new MonoCloudResourceExhaustedException(problemDetails),
      >= 500 => new MonoCloudServerException(problemDetails),
      _ => throw new System.Exception(string.IsNullOrEmpty(problemDetails.Title) ? "An Unknown Error Occurred" : problemDetails.Title)
    });

  /// <summary>
  /// Converts the error returned from the server into an exception
  /// </summary>
  /// <param name="statusCode">The response status code.</param>
  /// <param name="message">The error message returned from the server.</param>
  /// <returns></returns>
  public static MonoCloudException ThrowErr(int statusCode, string? message = null) =>
    throw (statusCode switch
    {
      400 => new MonoCloudBadRequestException(message ?? "Bad Request"),
      401 => new MonoCloudUnauthorizedException(message ?? "Unauthorized"),
      403 => new MonoCloudForbiddenException(message ?? "Forbidden"),
      404 => new MonoCloudNotFoundException(message ?? "Not Found"),
      409 => new MonoCloudConflictException(message ?? "Conflict"),
      422 => new MonoCloudModelStateException(message ?? "Unprocessible entity"),
      429 => new MonoCloudResourceExhaustedException(message ?? "Resource Exhausted"),
      >= 500 => new MonoCloudServerException(message ?? "Server Error"),
      _ => throw new System.Exception(string.IsNullOrEmpty(message) ? "An Unknown Error Occurred" : message)
    });
}
