using System;

namespace MonoCloud.SDK.Core.Base;

/// <summary>
/// The MonoCloud Configuration
/// </summary>
public class MonoCloudConfig
{
  /// <summary>
  /// Initializes the MonoCloud Configuration
  /// </summary>
  /// <param name="domain">The MonoCloud Tenant Domain.</param>
  /// <param name="apiKey">The MonoCloud Tenant Api Key.</param>
  /// <param name="timeout">An optional timeout after which the request will be aborted.</param>
  public MonoCloudConfig(string domain, string apiKey, TimeSpan? timeout = null)
  {
    Domain = SanitizeUrl(domain);
    ApiKey = apiKey;
    Timeout = timeout ?? TimeSpan.FromSeconds(10);
  }

  /// <summary>
  /// The MonoCloud Tenant Domain
  /// </summary>
  public string Domain { get; }

  /// <summary>
  /// The MonoCloud Tenant Api Key
  /// </summary>
  public string ApiKey { get; }

  /// <summary>
  /// An optional timeout after which the request will be aborted
  /// </summary>
  public TimeSpan Timeout { get; }

  private string SanitizeUrl(string url)
  {
    var u = url;

    if (!url.StartsWith("https://"))
    {
      u = "https://" + u;
    }

    if (url.EndsWith("/"))
    {
      u = u.Substring(0, u.Length - 1);
    }

    return u;
  }
}
