namespace Tests.User.Api.Parameters;

/// <summary>
/// The pagination parameters
/// </summary>
public record PaginationParameters
{
    /// <summary>
    /// The page number
    /// </summary>
    [Required, Range(0, int.MaxValue)]
    public int Page { get; set; }

    /// <summary>
    /// The page size
    /// </summary>
    [Required, Range(1, 100)]
    public int PageSize { get; set; }

    /// <summary>
    /// The comma separated list of fields to order the result by (postfix a + for ascending or a - for descending) [i.e. +firstname,-lastname]
    /// </summary>
    public string OrderBy { get; set; } = string.Empty;
}