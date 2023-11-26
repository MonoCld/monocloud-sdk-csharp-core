using MonoCloud.SDK.Core.Models;

namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Resource Exhausted Exception
/// </summary>
public class MonoCloudResourceExhaustedException : MonoCloudRequestException
{
  /// <summary>
  /// Initializes the MonoCloudResourceExhaustedException Class
  /// </summary>
  /// <param name="response">The problem details returned from the server.</param>
  public MonoCloudResourceExhaustedException(ProblemDetails response) : base(response)
  {
  }
}
