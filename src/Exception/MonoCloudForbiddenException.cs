using MonoCloud.Core.Base;

namespace MonoCloud.Core.Exception;

public class MonoCloudForbiddenException : MonoCloudRequestException
{
  public MonoCloudForbiddenException(ProblemDetails response) : base(response)
  {
  }
}
