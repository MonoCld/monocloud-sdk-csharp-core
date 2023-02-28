using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MonoCloud.Core.Base;

public class ProblemDetails
{
  public string Type { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public int Status { get; set; }
  public string Detail { get; set; } = string.Empty;
  public string Instance { get; set; } = string.Empty;
  public IDictionary<string, string[]>? Errors { get; set; }

  [JsonExtensionData]
  public IDictionary<string, object> ExtensionData { get; set; } = new Dictionary<string, object>();
}
