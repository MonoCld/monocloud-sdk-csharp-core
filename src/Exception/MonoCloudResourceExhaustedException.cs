using MonoCloud.Core.Base;

namespace MonoCloud.Core.Exception;

public class MonoCloudResourceExhaustedException : MonoCloudRequestException
{
  public MonoCloudResourceExhaustedException(ProblemDetails response) : base(response)
  {
  }
}
