using System;

namespace MonoCloud.Core.Base;

public class MonoCloudConfig
{

  public MonoCloudConfig(string tenantId, string apiKey, TimeSpan? timeout = null)
  {
    TenantId = tenantId;
    ApiKey = apiKey;
    Timeout = timeout ?? TimeSpan.FromSeconds(5);
  }

  public string TenantId { get; }
  public string ApiKey { get; }
  public TimeSpan Timeout { get; }
}
