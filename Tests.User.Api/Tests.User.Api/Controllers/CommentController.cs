namespace Tests.User.Api.Controllers;

/// <summary>
///     The book comments controller
/// </summary>
/// <param name="mediator">A reference to the mediator object</param>
[ApiController]
[Route("api/comments")]
public sealed class CommentController(IMediator mediator) : Controller
{
    /// <summary>
    ///     Create a new comment
    /// </summary>
    /// <param name="createCommentDto">An object representing the user that is going to be created.</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CommentDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Create([FromBody] CreateCommentDto createCommentDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await mediator
                            .Send(new CreateCommentRequest(createCommentDto), cancellationToken)
                            .ConfigureAwait(false);

        if (response.IsSuccessful)
        {
            return Ok(response.Payload!.ToDto());
        }

        return response.Exception == null
                   ? BadRequest(new ProblemDetails { Status = 400, Title = response.Error })
                   : Problem(statusCode: 500, title: "Cannot create comment.", detail: response.Exception!.Message);
    }

    /// <summary>
    ///     Deletes a comment
    /// </summary>
    /// <param name="id">ID of the comment</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var response = await mediator
                            .Send(new DeleteCommentRequest(id), cancellationToken)
                            .ConfigureAwait(false);

        return response.IsSuccessful
                   ? NoContent()
                   : Problem(statusCode: 500, title: "Cannot delete comment.", detail: response.Exception!.Message);
    }
}