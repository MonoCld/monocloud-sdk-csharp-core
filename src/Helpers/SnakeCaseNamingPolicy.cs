using System.Text.Json;

namespace MonoCloud.Core.Helpers;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
  public override string ConvertName(string name) => name.ToSnakeCase();
}
