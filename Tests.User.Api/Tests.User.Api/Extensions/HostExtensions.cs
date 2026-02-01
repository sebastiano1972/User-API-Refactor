namespace Tests.User.Api.Extensions;

internal static class HostExtensions
{
    public static async Task SeedDb(this IHost host)
    {
        Domain.Entities.User? user1;
        Domain.Entities.User? user2;
        Domain.Entities.User? user3;

        Book? book1;
        Book? book2;

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            user1 = (await mediator
                        .Send(new CreateUserRequest(new CreateUserDto
                                                    {
                                                        FirstName = "Sebastiano",
                                                        LastName = "Serri",
                                                        Age = 54
                                                    }))).Payload;
        }

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            user2 = (await mediator
                        .Send(new CreateUserRequest(new CreateUserDto
                                                    {
                                                        FirstName = "John",
                                                        LastName = "Doe",
                                                        Age = 54
                                                    }))).Payload;
        }

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            user3 = (await mediator
                        .Send(new CreateUserRequest(new CreateUserDto
                                                    {
                                                        FirstName = "Jane",
                                                        LastName = "Doe",
                                                        Age = 54
                                                    }))).Payload;
        }

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            book1 = (await mediator
                        .Send(new CreateBookRequest(new CreateBookDto()
                                                    {
                                                        Title = "Dune",
                                                        Author = "Frank Herbert",
                                                    }))).Payload;
        }

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator
               .Send(new CreateCommentRequest(new CreateCommentDto
                                              {
                                                  UserId = user1!.Id,
                                                  BookId = book1!.Id,
                                                  Title = "A great book!",
                                                  Content = "Nothing to add."
                                              }));
        }

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator
               .Send(new CreateCommentRequest(new CreateCommentDto
                                              {
                                                  UserId = user2!.Id,
                                                  BookId = book1!.Id,
                                                  Title = "Superb",
                                                  Content = "What should I say."
                                              }));
        }

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator
               .Send(new CreateCommentRequest(new CreateCommentDto
                                              {
                                                  UserId = user3!.Id,
                                                  BookId = book1!.Id,
                                                  Title = "Boring!",
                                                  Content = "Cannot finish it!"
                                              }));
        }

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            book2 = (await mediator
                        .Send(new CreateBookRequest(new CreateBookDto()
                                                    {
                                                        Title = "The heretics of Dune",
                                                        Author = "Frank Herbert",
                                                    }))).Payload;
        }

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator
               .Send(new CreateCommentRequest(new CreateCommentDto
                                              {
                                                  UserId = user1!.Id,
                                                  BookId = book2!.Id,
                                                  Title = "A great sequel!",
                                                  Content = "Adding something would be too much."
                                              }));
        }

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator
               .Send(new CreateCommentRequest(new CreateCommentDto
                                              {
                                                  UserId = user2!.Id,
                                                  BookId = book2!.Id,
                                                  Title = "Not so great",
                                                  Content = "I liked Dune more."
                                              }));
        }

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator
               .Send(new CreateCommentRequest(new CreateCommentDto
                                              {
                                                  UserId = user3!.Id,
                                                  BookId = book2!.Id,
                                                  Title = "Another boring book!",
                                                  Content = "Cannot finish this book either!"
                                              }));
        }

        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator
               .Send(new BorrowABookRequest(new BorrowABookDto
                                            {
                                                UserId = user1!.Id,
                                                BookId = book1!.Id
                                            }));
        }
    }
}