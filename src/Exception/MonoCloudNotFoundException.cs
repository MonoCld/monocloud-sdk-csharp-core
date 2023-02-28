using MonoCloud.Core.Base;

namespace MonoCloud.Core.Exception;

public class MonoCloudNotFoundException : MonoCloudRequestException
{
  public MonoCloudNotFoundException(ProblemDetails response) : base(response)
  {
  }
}
