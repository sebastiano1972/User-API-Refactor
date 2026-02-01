namespace Tests.User.Api.Extensions;

internal static class HostExtensions
{
    public static async Task SeedDb(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var user1 = (await mediator
           .Send(new CreateUserRequest(new CreateUserDto
                                       {
                                           FirstName = "Sebastiano",
                                           LastName = "Serri",
                                           Age = 54
                                       }))).Payload;

        var user2 = (await mediator
           .Send(new CreateUserRequest(new CreateUserDto
                                       {
                                           FirstName = "John",
                                           LastName = "Doe",
                                           Age = 54
                                       }))).Payload;

        var user3 = (await mediator
           .Send(new CreateUserRequest(new CreateUserDto
                                       {
                                           FirstName = "Jane",
                                           LastName = "Doe",
                                           Age = 54
                                       }))).Payload;

        var book1 = (await mediator
                       .Send(new CreateBookRequest(new CreateBookDto()
                                                   {
                                                       Title = "Dune",
                                                       Author = "Frank Herbert",
                                                   }))).Payload;

        await mediator
           .Send(new CreateCommentRequest(new CreateCommentDto
                                          {
                                              UserId = user1!.Id,
                                              BookId = book1!.Id,
                                              Title = "A great book!",
                                              Content = "Nothing to add."
                                          }));

        await mediator
           .Send(new CreateCommentRequest(new CreateCommentDto
                                          {
                                              UserId = user2!.Id,
                                              BookId = book1!.Id,
                                              Title = "Superb",
                                              Content = "What should I say."
                                          }));

        await mediator
           .Send(new CreateCommentRequest(new CreateCommentDto
                                          {
                                              UserId = user3!.Id,
                                              BookId = book1!.Id,
                                              Title = "Boring!",
                                              Content = "Cannot finish it!"
                                          }));

        var book2 = (await mediator
                        .Send(new CreateBookRequest(new CreateBookDto()
                                                    {
                                                        Title = "The heretics of Dune",
                                                        Author = "Frank Herbert",
                                                    }))).Payload;

        await mediator
           .Send(new CreateCommentRequest(new CreateCommentDto
                                          {
                                              UserId = user1!.Id,
                                              BookId = book2!.Id,
                                              Title = "A great sequel!",
                                              Content = "Adding something would be too much."
                                          }));

        await mediator
           .Send(new CreateCommentRequest(new CreateCommentDto
                                          {
                                              UserId = user2!.Id,
                                              BookId = book2!.Id,
                                              Title = "Not so great",
                                              Content = "I liked Dune more."
                                          }));

        await mediator
           .Send(new CreateCommentRequest(new CreateCommentDto
                                          {
                                              UserId = user3!.Id,
                                              BookId = book2!.Id,
                                              Title = "Another boring book!",
                                              Content = "Cannot finish this book either!"
                                          }));

        await mediator
           .Send(new BorrowABookRequest(new BorrowABookDto
                                        {
                                            UserId = user1!.Id,
                                            BookId = book1!.Id
                                        }));
    }
}