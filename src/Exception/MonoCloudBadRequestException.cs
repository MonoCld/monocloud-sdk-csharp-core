using MonoCloud.Core.Base;

namespace MonoCloud.Core.Exception;

public class MonoCloudBadRequestException : MonoCloudRequestException
{
  public MonoCloudBadRequestException(ProblemDetails response) : base(response)
  {
  }
}
