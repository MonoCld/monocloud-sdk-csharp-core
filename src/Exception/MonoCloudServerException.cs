using MonoCloud.Core.Base;

namespace MonoCloud.Core.Exception;

public class MonoCloudServerException : MonoCloudRequestException
{
  public MonoCloudServerException(ProblemDetails response) : base(response)
  {
  }
}
