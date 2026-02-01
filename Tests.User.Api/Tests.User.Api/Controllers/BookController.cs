namespace Tests.User.Api.Controllers;

/// <summary>
///     The books controller
/// </summary>
/// <param name="mediator">A reference to the mediator object</param>
[ApiController]
[Route("api/books")]
public sealed class BookController(IMediator mediator) : Controller
{
    /// <summary>
    ///     Gets all books
    /// </summary>
    /// <param name="paginationParameters">The pagination parameters</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookListDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParameters paginationParameters, CancellationToken cancellationToken)
    {
        var response = await mediator
                            .Send(new GetBooksRequest(paginationParameters.Page, paginationParameters.PageSize, paginationParameters.OrderBy), cancellationToken)
                            .ConfigureAwait(false);

        if (response.IsSuccessful)
        {
            return Ok(response.Payload);
        }

        return Problem(statusCode: response.Exception is MalformedOrderByParameterException ? 400 : 500,
                       title: "Cannot retrieve books list.",
                       detail: response.Exception!.Message);
    }

    /// <summary>
    ///     Gets a book
    /// </summary>
    /// <param name="id">ID of the book</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpGet("id:int")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var response = await mediator
                            .Send(new GetBookRequest(id), cancellationToken)
                            .ConfigureAwait(false);

        if (response.IsSuccessful)
        {
            return Ok(response.Payload);
        }

        return response.NotFound
                   ? NotFound(new ProblemDetails { Status = 404, Title = "Book not found." })
                   : Problem(statusCode: 500, title: "Cannot retrieve book.", detail: response.Exception!.Message);
    }

    /// <summary>
    ///     Create a new book
    /// </summary>
    /// <param name="createBookDto">An object representing the book that is going to be created.</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookListDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Create([FromBody] CreateBookDto createBookDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await mediator
                            .Send(new CreateBookRequest(createBookDto), cancellationToken)
                            .ConfigureAwait(false);

        return response.IsSuccessful
                   ? CreatedAtAction(nameof(Get), new { id = response.Payload!.Id }, response.Payload.ToListDto())
                   : Problem(statusCode: 500, title: "Cannot create book.", detail: response.Exception!.Message);
    }

    /// <summary>
    ///     Updates a book
    /// </summary>
    /// <param name="id">ID of the book</param>
    /// <param name="updateBookDto">An object representing the book that is going to be updated.</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookListDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookDto updateBookDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await mediator
                            .Send(new UpdateBookRequest(id, updateBookDto), cancellationToken)
                            .ConfigureAwait(false);

        if (response.IsSuccessful)
        {
            return Ok(response.Payload!.ToListDto());
        }

        return response.Exception == null
                   ? BadRequest(new ProblemDetails { Status = 400, Title = response.Error })
                   : Problem(statusCode: 500, title: "Cannot update book.", detail: response.Exception!.Message);
    }

    /// <summary>
    ///     Deletes a book
    /// </summary>
    /// <param name="id">ID of the book</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var response = await mediator
                            .Send(new DeleteBookRequest(id), cancellationToken)
                            .ConfigureAwait(false);

        return response.IsSuccessful
                   ? NoContent()
                   : Problem(statusCode: 500, title: "Cannot delete book.", detail: response.Exception!.Message);
    }
}