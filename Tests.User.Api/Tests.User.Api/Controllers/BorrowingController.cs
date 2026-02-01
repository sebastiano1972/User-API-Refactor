namespace Tests.User.Api.Controllers;

/// <summary>
///     The user controller
/// </summary>
/// <param name="mediator">A reference to the mediator object</param>
[ApiController]
[Route("api/borrowings")]
public sealed class BorrowingController(IMediator mediator) : Controller
{
    /// <summary>
    ///     Borrow a book
    /// </summary>
    /// <param name="borrowABookDto">An object representing the book borrowing.</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Create([FromBody] BorrowABookDto borrowABookDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await mediator
                            .Send(new BorrowABookRequest(borrowABookDto), cancellationToken)
                            .ConfigureAwait(false);

        return response.IsSuccessful
                   ? Ok()
                   : Problem(statusCode: 500, title: "Cannot borrow the book.", detail: response.Exception!.Message);
    }

    /// <summary>
    ///     Returns a book
    /// </summary>
    /// <param name="returnABookDto">An object representing the book return.</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Delete([FromBody] ReturnABookDto returnABookDto, CancellationToken cancellationToken)
    {
        var response = await mediator
                            .Send(new ReturnABookRequest(returnABookDto), cancellationToken)
                            .ConfigureAwait(false);

        return response.IsSuccessful
                   ? NoContent()
                   : Problem(statusCode: 500, title: "Cannot return the book.", detail: response.Exception!.Message);
    }
}