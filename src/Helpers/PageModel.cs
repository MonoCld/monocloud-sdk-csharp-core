namespace MonoCloud.SDK.Core.Helpers;

public class PageModel
{
  /// <summary>
  /// Page Size
  /// </summary>
  public int PageSize { get; set; }

  /// <summary>
  /// Current Page
  /// </summary>
  public int CurrentPage { get; set; }

  /// <summary>
  /// Total Number of Items
  /// </summary>
  public int TotalCount { get; set; }

  /// <summary>
  /// Specfies whether there is a previous page
  /// </summary>
  public bool HasPrevious { get; set; }

  /// <summary>
  /// Specfies whether there is a next page
  /// </summary>
  public bool HasNext { get; set; }
}
