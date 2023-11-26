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
      422 => new MonoCloudModelStateException(problemDetails),
      429 => new MonoCloudResourceExhaustedException(problemDetails),
      >= 500 => new MonoCloudServerException(problemDetails),
      _ => throw new System.Exception(string.IsNullOrEmpty(problemDetails.Title) ? "An Unknown Error Occurred" : problemDetails.Title)
    });
}
