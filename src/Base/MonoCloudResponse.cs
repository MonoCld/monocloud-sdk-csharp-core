using System.Collections.Generic;

namespace MonoCloud.Core.Base;

public class MonoCloudResponse<TResult>
{
  public MonoCloudResponse(int status, IDictionary<string, IEnumerable<string>> headers, TResult result)
  {
    Headers = headers;
    Status = status;
    Result = result;
  }

  public int Status { get; }
  public IDictionary<string, IEnumerable<string>> Headers { get; }
  public TResult Result { get; }
}
