using MonoCloud.Core.Base;

namespace MonoCloud.Core.Exception;

public class MonoCloudConflictException : MonoCloudRequestException
{
  public MonoCloudConflictException(ProblemDetails response) : base(response)
  {
  }
}
