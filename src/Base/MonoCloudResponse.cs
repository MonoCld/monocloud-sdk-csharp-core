using MonoCloud.SDK.Core.Helpers;
using System.Collections.Generic;

namespace MonoCloud.SDK.Core.Base;

/// <summary>
/// The MonoCloud Response
/// </summary>
public class MonoCloudResponse
{
  /// <summary>
  /// Initializes the MonoCloud Response
  /// </summary>
  /// <param name="status">The Status Code returned from the server.</param>
  /// <param name="headers">The Headers returned from the server.</param>
  public MonoCloudResponse(int status, IDictionary<string, IEnumerable<string>> headers)
  {
    Headers = headers;
    Status = status;
  }

  /// <summary>
  /// The Status Code returned from the server
  /// </summary>
  public int Status { get; }

  /// <summary>
  /// The Headers returned from the server
  /// </summary>
  public IDictionary<string, IEnumerable<string>> Headers { get; }
}

/// <summary>
/// The MonoCloud Response
/// </summary>
/// <typeparam name="TResult">The type of the response</typeparam>
public class MonoCloudResponse<TResult> : MonoCloudResponse
{
  /// <summary>
  /// Initializes the MonoCloudResponse class
  /// </summary>
  /// <param name="status">The Status Code returned from the server.</param>
  /// <param name="headers">The Headers returned from the server.</param>
  /// <param name="data">The data returned from the server.</param>
  public MonoCloudResponse(int status, IDictionary<string, IEnumerable<string>> headers, TResult data) : base(status, headers)
  {
    Data = data;
  }

  /// <summary>
  /// The data returned from the server
  /// </summary>
  public TResult Data { get; }
}

/// <summary>
/// The MonoCloud Response
/// </summary>
/// <typeparam name="TResult">The type of the response</typeparam>
/// <typeparam name="TPage">The type of the paging response</typeparam>
public class MonoCloudResponse<TResult, TPage> : MonoCloudResponse<TResult> where TPage : PageModel
{
  /// <summary>
  /// Initializes the MonoCloudResponse class
  /// </summary>
  /// <param name="status">The Status Code returned from the server.</param>
  /// <param name="headers">The Headers returned from the server.</param>
  /// <param name="data">The data returned from the server.</param>
  /// <param name="pageData">The pagination data returned from the server.</param>
  public MonoCloudResponse(int status, IDictionary<string, IEnumerable<string>> headers, TResult data, TPage pageData) : base(status, headers, data)
  {
    PageData = pageData;
  }

  /// <summary>
  /// The pagination details returned from the server
  /// </summary>
  public TPage PageData { get; }
}
