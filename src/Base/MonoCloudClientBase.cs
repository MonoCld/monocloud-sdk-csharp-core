using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MonoCloud.Core.Exception;
using MonoCloud.Core.Helpers;

namespace MonoCloud.Core.Base;

public class MonoCloudClientBase
{

  // ReSharper disable once InconsistentNaming
  private static readonly JsonSerializerOptions _settings = new()
  {
    Converters = { new JsonStringEnumMemberConverter(new SnakeCaseNamingPolicy(), false) },
    PropertyNameCaseInsensitive = false,
    PropertyNamingPolicy = new SnakeCaseNamingPolicy()
  };

  // ReSharper disable once InconsistentNaming
  private readonly HttpClient _httpClient = new();

  protected MonoCloudClientBase(MonoCloudConfig configuration, string? baseUrl = null, HttpClient? httpClient = null)
  {
    if (string.IsNullOrEmpty(configuration.TenantId))
    {
      throw new MonoCloudException("Tenant ID is required");
    }

    if (string.IsNullOrEmpty(configuration.ApiKey))
    {
      throw new MonoCloudException("API Key is required");
    }

    if (httpClient is not null)
    {
      _httpClient = httpClient;
    }

    if (baseUrl is not null)
    {
      _httpClient.BaseAddress = new Uri(baseUrl.EndsWith("/") ? baseUrl : baseUrl + "/");
    }

    if (!_httpClient.DefaultRequestHeaders.Contains("X-TENANT-ID"))
    {
      _httpClient.DefaultRequestHeaders.Add("X-TENANT-ID", configuration.TenantId);
    }

    if (!_httpClient.DefaultRequestHeaders.Contains("X-API-KEY"))
    {
      _httpClient.DefaultRequestHeaders.Add("X-API-KEY", configuration.ApiKey);
    }

    if (httpClient is null)
    {
      _httpClient.Timeout = configuration.Timeout;
    }
  }

  protected string Serialize(object any) => JsonSerializer.Serialize(any, _settings);

  protected async Task<MonoCloudResponse<TResult>> ProcessRequestAsync<TResult>(HttpRequestMessage request, CancellationToken cancellationToken)
  {
    var response = await _httpClient.SendAsync(request, cancellationToken);

    if (!response.IsSuccessStatusCode)
    {
      await HandleErrorResponse(response, cancellationToken);
      response.Dispose();

      throw new MonoCloudException(
        $"Something went wrong, Received Status Code: {response.StatusCode}, {response.ReasonPhrase}");
    }

    using var responseStream = await response.Content.ReadAsStreamAsync();
    var result = await JsonSerializer.DeserializeAsync<TResult>(responseStream, _settings, cancellationToken);

    if (result is null)
    {
      throw new MonoCloudException("Invalid response body");
    }

    response.Dispose();

    return new MonoCloudResponse<TResult>((int)response.StatusCode,
      response.Headers.Concat(response.Content.Headers).ToDictionary(k => k.Key, v => v.Value), result);
  }

  private static async Task HandleErrorResponse(HttpResponseMessage response, CancellationToken cancellationToken)
  {
    if (response.Content.Headers.ContentType?.MediaType == "application/problem+json")
    {
      using var responseStream = await response.Content.ReadAsStreamAsync();

      var result =
        await JsonSerializer.DeserializeAsync<ProblemDetails>(responseStream, _settings, cancellationToken);

      response.Dispose();

      throw result is null
        ? new MonoCloudException("Invalid Problem Details body")
        : MonoCloudException.ThrowErr(result);
    }
  }
}
