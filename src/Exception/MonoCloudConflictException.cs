using MonoCloud.SDK.Core.Models;

namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Conflict Exception
/// </summary>
public class MonoCloudConflictException : MonoCloudRequestException
{
  /// <summary>
  /// Initializes the MonoCloudConflictException Class
  /// </summary>
  /// <param name="response">The problem details returned from the server.</param>
  public MonoCloudConflictException(ProblemDetails response) : base(response)
  {
  }

  /// <summary>
  /// Initializes the MonoCloudConflictException Class
  /// </summary>
  /// <param name="message">The error message returned from the server.</param>
  public MonoCloudConflictException(string message) : base(message)
  {
  }
}
