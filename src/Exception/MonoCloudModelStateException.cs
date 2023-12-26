namespace MonoCloud.SDK.Core.Exception;

/// <summary>
/// The MonoCloud Model State Exception
/// </summary>
public class MonoCloudModelStateException : MonoCloudRequestException
{
  /// <summary>
  /// Initializes the MonoCloudModelStateException Class
  /// </summary>
  /// <param name="message">The error message returned from the server.</param>
  public MonoCloudModelStateException(string message) : base(message)
  {
  }
}
