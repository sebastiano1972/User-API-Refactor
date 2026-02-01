using Tests.User.Application.Exceptions;

namespace Tests.User.Api.Controllers;

/// <summary>
///     The user controller
/// </summary>
/// <param name="mediator">A reference to the mediator object</param>
[ApiController]
[Route("api/users")]
public sealed class UserController(IMediator mediator) : Controller
{
    /// <summary>
    ///     Gets all users
    /// </summary>
    /// <param name="paginationParameters">The pagination parameters</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParameters paginationParameters, CancellationToken cancellationToken)
    {
        var response = await mediator
                            .Send(new GetUsersRequest(paginationParameters.Page, paginationParameters.PageSize, paginationParameters.OrderBy), cancellationToken)
                            .ConfigureAwait(false);

        if (response.IsSuccessful)
        {
            return Ok(response.Payload!.Select(u => u.ToDto()).ToList());
        }

        return Problem(statusCode: response.Exception is MalformedOrderByParameterException ? 400 : 500,
                       title: "Cannot retrieve users list.",
                       detail: response.Exception!.Message);
    }

    /// <summary>
    ///     Gets a user
    /// </summary>
    /// <param name="id">ID of the user</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpGet("id:int")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var response = await mediator
                            .Send(new GetUserRequest(id), cancellationToken)
                            .ConfigureAwait(false);

        if (response.IsSuccessful)
        {
            return Ok(response.Payload!.ToDto());
        }

        return response.UserNotFound
                   ? NotFound(new ProblemDetails { Status = 404, Title = "User not found." })
                   : Problem(statusCode: 500, title: "Cannot retrieve user.", detail: response.Exception!.Message);
    }

    /// <summary>
    ///     Create a new user
    /// </summary>
    /// <param name="createUserDto">An object representing the user that is going to be created.</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await mediator
                            .Send(new CreateUserRequest(createUserDto), cancellationToken)
                            .ConfigureAwait(false);

        return response.IsSuccessful
                   ? CreatedAtAction(nameof(Get), new { id = response.Payload!.Id }, response.Payload.ToDto())
                   : Problem(statusCode: 500, title: "Cannot create user.", detail: response.Exception!.Message);
    }

    /// <summary>
    ///     Updates a user
    /// </summary>
    /// <param name="id">ID of the user</param>
    /// <param name="updateUserDto">An object representing the user that is going to be updated.</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await mediator
                            .Send(new UpdateUserRequest(id, updateUserDto), cancellationToken)
                            .ConfigureAwait(false);

        if (response.IsSuccessful)
        {
            return Ok(response.Payload!.ToDto());
        }

        return response.Exception == null
                   ? BadRequest(new ProblemDetails { Status = 400, Title = response.Error })
                   : Problem(statusCode: 500, title: "Cannot update user.", detail: response.Exception!.Message);
    }

    /// <summary>
    ///     Deletes a user
    /// </summary>
    /// <param name="id">ID of the user</param>
    /// <param name="cancellationToken">A cancellation token</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var response = await mediator
                            .Send(new DeleteUserRequest(id), cancellationToken)
                            .ConfigureAwait(false);

        return response.IsSuccessful
                   ? NoContent()
                   : Problem(statusCode: 500, title: "Cannot create user.", detail: response.Exception!.Message);
    }
}