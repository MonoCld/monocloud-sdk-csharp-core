using MonoCloud.SDK.Core.Models;

namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Not Found Exception
/// </summary>
public class MonoCloudNotFoundException : MonoCloudRequestException
{
  /// <summary>
  /// Initializes the MonoCloudNotFoundException Class
  /// </summary>
  /// <param name="response">The problem details returned from the server.</param>
  public MonoCloudNotFoundException(ProblemDetails response) : base(response)
  {
  }
}
