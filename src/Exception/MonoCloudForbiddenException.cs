using MonoCloud.SDK.Core.Models;

namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Forbidden Exception
/// </summary>
public class MonoCloudForbiddenException : MonoCloudRequestException
{
  /// <summary>
  /// Initializes the MonoCloudForbiddenException Class
  /// </summary>
  /// <param name="response">The problem details returned from the server.</param>
  public MonoCloudForbiddenException(ProblemDetails response) : base(response)
  {
  }
}
