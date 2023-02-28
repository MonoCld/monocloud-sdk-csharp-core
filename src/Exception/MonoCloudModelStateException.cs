using MonoCloud.Core.Base;

namespace MonoCloud.Core.Exception;

public class MonoCloudModelStateException : MonoCloudRequestException
{
  public MonoCloudModelStateException(ProblemDetails response) : base(response)
  {
  }
}
