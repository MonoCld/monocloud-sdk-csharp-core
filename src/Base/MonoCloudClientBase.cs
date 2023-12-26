using MonoCloud.SDK.Core.Exception;
using MonoCloud.SDK.Core.Helpers;
using MonoCloud.SDK.Core.Models;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MonoCloud.SDK.Core.Base;

/// <summary>
/// The MonoCloud Client Base Class
/// </summary>
public class MonoCloudClientBase
{
  private static readonly JsonSerializerOptions Settings = new()
  {
    Converters =
    {
      new JsonStringEnumMemberConverter(new SnakeCaseNamingPolicy(), false),
      new EpochDateTimeNullableConverter(),
      new EpochDateTimeConverter()
    },
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNameCaseInsensitive = false,
    PropertyNamingPolicy = new SnakeCaseNamingPolicy()
  };

  private readonly HttpClient _httpClient;

  /// <summary>
  /// Initializes the MonoCloud Client Base Class
  /// </summary>
  /// <param name="configuration">The <see cref="MonoCloudConfig">MonoCloud Configuration</see></param>
  /// <exception cref="MonoCloudException"></exception>
  protected MonoCloudClientBase(MonoCloudConfig configuration)
  {
     if (configuration is null)
     {
       throw new MonoCloudException("Configuration is required");
     }

     if (string.IsNullOrWhiteSpace(configuration.Domain))
     {
       throw new MonoCloudException("Tenant Domain is required");
     }

     if (string.IsNullOrWhiteSpace(configuration.ApiKey))
     {
       throw new MonoCloudException("API Key is required");
     }

     _httpClient = new HttpClient
     {
       BaseAddress = new Uri($"{configuration.Domain}/api/"),
       Timeout = configuration.Timeout,
     };

     _httpClient.DefaultRequestHeaders.Add("X-API-KEY", configuration.ApiKey);
  }

  /// <summary>
  /// Initializes the MonoCloud Client Base Class
  /// </summary>
  /// <param name="httpClient">The <see cref="HttpClient"/> which will be used to communicate with the MonoCloud Api</param>
  /// <exception cref="MonoCloudException"></exception>
  protected MonoCloudClientBase(HttpClient httpClient)
  {
    _httpClient = httpClient ?? throw new MonoCloudException("HttpClient is required");
  }

  /// <summary>
  /// The serialize method which will serialize an object to Json
  /// </summary>
  /// <param name="data">The data to be serialized.</param>
  /// <returns></returns>
  protected string Serialize(object data) => JsonSerializer.Serialize(data, Settings);

  /// <summary>
  /// The Process Request Method which processes the request provided.
  /// </summary>
  /// <param name="request">The <see cref="HttpRequestMessage"/> to be processed.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <typeparam name="TResult">The return type of response.</typeparam>
  /// <returns>A <see cref="MonoCloudResponse"/> of type <typeparamref name="TResult"/></returns>
  /// <exception cref="MonoCloudException"></exception>
  protected async Task<MonoCloudResponse<TResult>> ProcessRequestAsync<TResult>(HttpRequestMessage request, CancellationToken cancellationToken = default)
  {
    var response = await _httpClient.SendAsync(request, cancellationToken);

    request.Dispose();

    if (!response.IsSuccessStatusCode)
    {
      await HandleErrorResponse(response, cancellationToken);
      response.Dispose();

      throw new MonoCloudException($"Something went wrong, Received Status Code: {response.StatusCode}, {response.ReasonPhrase}");
    }

    using var responseStream = await response.Content.ReadAsStreamAsync();

    var result = await JsonSerializer.DeserializeAsync<TResult>(responseStream, Settings, cancellationToken);

    if (result is null)
    {
      throw new MonoCloudException("Invalid response body");
    }

    response.Dispose();

    return new MonoCloudResponse<TResult>((int)response.StatusCode, response.Headers.Concat(response.Content.Headers).ToDictionary(k => k.Key, v => v.Value), result);
  }

  /// <summary>
  /// The Process Request Method which processes the request provided.
  /// </summary>
  /// <param name="request">The <see cref="HttpRequestMessage"/> to be processed.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <typeparam name="TResult">The return type of response.</typeparam>
  /// <typeparam name="TPage">The pagination data.</typeparam>
  /// <returns>A <see cref="MonoCloudResponse"/> of type <typeparamref name="TResult"/></returns>
  /// <exception cref="MonoCloudException"></exception>
  protected async Task<MonoCloudResponse<TResult, TPage>> ProcessRequestAsync<TResult, TPage>(HttpRequestMessage request, CancellationToken cancellationToken = default) where TPage : PageModel, new()
  {
    var response = await _httpClient.SendAsync(request, cancellationToken);

    request.Dispose();

    if (!response.IsSuccessStatusCode)
    {
      await HandleErrorResponse(response, cancellationToken);
      response.Dispose();

      throw new MonoCloudException($"Something went wrong, Received Status Code: {response.StatusCode}, {response.ReasonPhrase}");
    }

    using var responseStream = await response.Content.ReadAsStreamAsync();

    var result = await JsonSerializer.DeserializeAsync<TResult>(responseStream, Settings, cancellationToken);

    if (result is null)
    {
      throw new MonoCloudException("Invalid response body");
    }

    response.Dispose();

    TPage pageModel = new();
    var header = response.Headers.FirstOrDefault(x => x.Key.Equals("x-pagination", StringComparison.OrdinalIgnoreCase));
    var pageHeader = header.Value?.FirstOrDefault();

    if (pageHeader is not null)
    {
      pageModel = JsonSerializer.Deserialize<TPage>(pageHeader, Settings)!;
    }

    return new MonoCloudResponse<TResult, TPage>((int)response.StatusCode, response.Headers.Concat(response.Content.Headers).ToDictionary(k => k.Key, v => v.Value), result, pageModel);
  }

  /// <summary>
  /// The Process Request Method which processes the request provided.
  /// </summary>
  /// <param name="request">The <see cref="HttpRequestMessage"/> to be processed.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A <see cref="MonoCloudResponse"/></returns>
  /// <exception cref="MonoCloudException"></exception>
  protected async Task<MonoCloudResponse> ProcessRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
  {
    var response = await _httpClient.SendAsync(request, cancellationToken);

    request.Dispose();

    if (!response.IsSuccessStatusCode)
    {
      await HandleErrorResponse(response, cancellationToken);
      response.Dispose();

      throw new MonoCloudException($"Something went wrong, Received Status Code: {response.StatusCode}, {response.ReasonPhrase}");
    }

    response.Dispose();

    return new MonoCloudResponse((int)response.StatusCode, response.Headers.Concat(response.Content.Headers).ToDictionary(k => k.Key, v => v.Value));
  }

  private static async Task HandleErrorResponse(HttpResponseMessage response, CancellationToken cancellationToken)
  {
    if (response.Content.Headers.ContentType?.MediaType == "application/problem+json")
    {
      var responseBytes = await response.Content.ReadAsByteArrayAsync();

      var result = await JsonSerializer.DeserializeAsync<ProblemDetails>(new MemoryStream(responseBytes), Settings, cancellationToken);

      if (result is null)
      {
        throw new MonoCloudException("Invalid body");
      }

      result = result.Type switch
      {
        ValidationExceptionTypes.IdentityValidationError => await JsonSerializer.DeserializeAsync<ErrorCodeValidationProblemDetails>(new MemoryStream(responseBytes), Settings, cancellationToken),
        ValidationExceptionTypes.ValidationError => await JsonSerializer.DeserializeAsync<KeyValidationProblemDetails>(new MemoryStream(responseBytes), Settings, cancellationToken),
        _ => result
      };

      throw result is null
        ? new MonoCloudException("Invalid body")
        : MonoCloudException.ThrowErr(result);
    }

    var message = await response.Content.ReadAsStringAsync();

    MonoCloudException.ThrowErr((int)response.StatusCode, message is not null && message != string.Empty ? message : response.StatusCode.ToString());
  }
}
