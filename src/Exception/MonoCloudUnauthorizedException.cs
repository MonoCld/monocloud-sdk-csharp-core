using MonoCloud.Core.Base;

namespace MonoCloud.Core.Exception;

public class MonoCloudUnauthorizedException : MonoCloudRequestException
{
  public MonoCloudUnauthorizedException(ProblemDetails response) : base(response)
  {
  }
}
