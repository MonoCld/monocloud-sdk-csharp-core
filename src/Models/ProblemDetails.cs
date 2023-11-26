using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MonoCloud.SDK.Core.Models;

/// <summary>
/// The Problem Details
/// </summary>
public class ProblemDetails
{
  /// <summary>
  /// The type of error
  /// </summary>
  public string Type { get; set; } = string.Empty;

  /// <summary>
  /// The title of the error
  /// </summary>
  public string Title { get; set; } = string.Empty;

  /// <summary>
  /// The status code representing the error
  /// </summary>
  public int Status { get; set; }

  /// <summary>
  /// The error details
  /// </summary>
  public string Detail { get; set; } = string.Empty;

  /// <summary>
  /// The instance
  /// </summary>
  public string Instance { get; set; } = string.Empty;

  /// <summary>
  /// A collection of errors
  /// </summary>
  public IDictionary<string, string[]>? Errors { get; set; }

  /// <summary>
  /// Additional data about the error
  /// </summary>
  [JsonExtensionData]
  public IDictionary<string, object> ExtensionData { get; set; } = new Dictionary<string, object>();
}
