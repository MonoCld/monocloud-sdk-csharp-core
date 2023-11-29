using MonoCloud.SDK.Core.Models;

namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Unauthorized Exception
/// </summary>
public class MonoCloudUnauthorizedException : MonoCloudRequestException
{
  /// <summary>
  /// Initializes the MonoCloudUnauthorizedException Class
  /// </summary>
  /// <param name="response">The problem details returned from the server.</param>
  public MonoCloudUnauthorizedException(ProblemDetails response) : base(response)
  {
  }

  /// <summary>
  /// Initializes the MonoCloudUnauthorizedException Class
  /// </summary>
  /// <param name="message">The error message returned from the server.</param>
  public MonoCloudUnauthorizedException(string message) : base(message)
  {
  }
}
