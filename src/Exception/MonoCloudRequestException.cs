using MonoCloud.Core.Base;

namespace MonoCloud.Core.Exception;

public class MonoCloudRequestException : MonoCloudException
{
  public MonoCloudRequestException(ProblemDetails response) : base(response.Title)
  {
    Response = response;
  }

  public ProblemDetails Response { get; }
}
