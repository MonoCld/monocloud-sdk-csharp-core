using MonoCloud.SDK.Core.Models;

namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Server Exception
/// </summary>
public class MonoCloudServerException : MonoCloudRequestException
{
  /// <summary>
  /// Initializes the MonoCloudServerException Class
  /// </summary>
  /// <param name="response">The problem details returned from the server.</param>
  public MonoCloudServerException(ProblemDetails response) : base(response)
  {
  }
}
