namespace Tests.User.Api.Parameters;

public record PaginationParameters
{
    [Required, Range(0, int.MaxValue)]
    public int Page { get; set; }

    [Required, Range(1, 100)]
    public int PageSize { get; set; }
}