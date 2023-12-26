using MonoCloud.SDK.Core.Models;
using System.Collections.Generic;

namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Request Exception
/// </summary>
public class MonoCloudRequestException : MonoCloudException
{
  /// <summary>
  /// Initializes the MonoCloudRequestException Class
  /// </summary>
  /// <param name="response">The problem details returned from the server.</param>
  protected MonoCloudRequestException(ProblemDetails response) : base(response.Title)
  {
    Response = response;
  }

  /// <summary>
  /// Initializes the MonoCloudRequestException Class
  /// </summary>
  /// <param name="message">The error message returned from the server.</param>
  protected MonoCloudRequestException(string message) : base(message)
  {
  }

  /// <summary>
  /// The problem details received from the server.
  /// </summary>
  public ProblemDetails? Response { get; }
}
