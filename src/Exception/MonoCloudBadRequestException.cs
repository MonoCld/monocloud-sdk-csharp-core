using MonoCloud.SDK.Core.Models;

namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Bad Request Exception
/// </summary>
public class MonoCloudBadRequestException : MonoCloudRequestException
{
  /// <summary>
  /// Initializes the MonoCloudBadRequestException Class
  /// </summary>
  /// <param name="response">The problem details returned from the server.</param>
  public MonoCloudBadRequestException(ProblemDetails response) : base(response)
  {
  }
}
