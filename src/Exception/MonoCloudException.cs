using MonoCloud.Core.Base;

namespace MonoCloud.Core.Exception;

public class MonoCloudException : System.Exception
{
  public MonoCloudException(string message) : base(message)
  {
  }

  public static MonoCloudException ThrowErr(ProblemDetails problemDetails) =>
    throw (problemDetails.Status switch
    {
      400 => new MonoCloudBadRequestException(problemDetails),
      401 => new MonoCloudUnauthorizedException(problemDetails),
      403 => new MonoCloudForbiddenException(problemDetails),
      404 => new MonoCloudNotFoundException(problemDetails),
      409 => new MonoCloudConflictException(problemDetails),
      422 => new MonoCloudModelStateException(problemDetails),
      429 => new MonoCloudResourceExhaustedException(problemDetails),
      _ => new MonoCloudServerException(problemDetails)
    });
}
