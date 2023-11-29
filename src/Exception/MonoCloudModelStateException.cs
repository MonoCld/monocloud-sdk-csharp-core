using MonoCloud.SDK.Core.Models;

namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Model State Exception
/// </summary>
public class MonoCloudModelStateException : MonoCloudRequestException
{
  /// <summary>
  /// Initializes the MonoCloudModelStateException Class
  /// </summary>
  /// <param name="response">The problem details returned from the server.</param>
  public MonoCloudModelStateException(ProblemDetails response) : base(response)
  {
  }

  /// <summary>
  /// Initializes the MonoCloudModelStateException Class
  /// </summary>
  /// <param name="message">The error message returned from the server.</param>
  public MonoCloudModelStateException(string message) : base(message)
  {
  }
}
