namespace Tests.User.Api.Test;

public class UserControllerTests
{
    [Fact]
    public async Task Should_Return_User_When_Valid_Id_Passed()
    {
        const int userId = 1;

        var user = new global::Tests.User.Domain.Entities.User
                   {
                       FirstName = "John",
                       LastName = "Doe",
                       Age = 20
                   };

        var mediator = Substitute.For<IMediator>();

        mediator
           .Send(Arg.Is<GetUserRequest>(r=>r.Id == userId), CancellationToken.None)
           .Returns(Task.FromResult(GetUserResponse.Success(user)));

        var controller = new UserController(mediator);
        
        var result = await controller
                        .Get(userId, CancellationToken.None);

        var ok = result as OkObjectResult;           

        Assert.NotNull(ok);
        Assert.Equal(200, ok.StatusCode);
        Assert.Equal(user.FirstName, ((UserDto)ok.Value!).FirstName);
        Assert.Equal(user.LastName, ((UserDto)ok.Value!).LastName);
        Assert.Equal(user.Age, ((UserDto)ok.Value!).Age);
    }

    [Fact]
    public async Task Should_Return_Valid_When_User_Created()
    {
        var createUserDto = new CreateUserDto
                   {
                       FirstName = "John",
                       LastName = "Doe",
                       Age = 20
                   };

        var user = new global::Tests.User.Domain.Entities.User
                   {
                       FirstName = createUserDto.FirstName,
                       LastName = createUserDto.LastName,
                       Age = createUserDto.Age!.Value
        };

        var mediator = Substitute.For<IMediator>();

        mediator
           .Send(Arg.Is<CreateUserRequest>(r => r.Payload.FirstName == createUserDto.FirstName
                                             && r.Payload.LastName == createUserDto.LastName
                                             && r.Payload.Age == createUserDto.Age), CancellationToken.None)
           .Returns(Task.FromResult(CreateUserResponse.Success(user)));

        var controller = new UserController(mediator);

        var result = await controller.Create(createUserDto, CancellationToken.None);

        var ok = result as CreatedAtActionResult;

        Assert.NotNull(ok);
        Assert.Equal(201, ok.StatusCode);
        Assert.Equal(user.FirstName, ((UserDto)ok.Value!).FirstName);
        Assert.Equal(user.LastName, ((UserDto)ok.Value!).LastName);
        Assert.Equal(user.Age, ((UserDto)ok.Value!).Age);
    }

    [Fact]
    public async Task Should_Return_Valid_When_User_Updated()
    {
        var updateUserDto = new UpdateUserDto
                            {
                                FirstName = "John",
                                LastName = "Doe",
                                Age = 20
                            };

        var user = new global::Tests.User.Domain.Entities.User
                   {
                       Id = 1,
                       FirstName = updateUserDto.FirstName,
                       LastName = updateUserDto.LastName,
                       Age = updateUserDto.Age!.Value
                   };

        var mediator = Substitute.For<IMediator>();

        mediator
           .Send(Arg.Is<UpdateUserRequest>(r => r.Payload.FirstName == updateUserDto.FirstName
                                             && r.Payload.LastName == updateUserDto.LastName
                                             && r.Payload.Age == updateUserDto.Age), CancellationToken.None)
           .Returns(Task.FromResult(UpdateUserResponse.Success(user)));

        var controller = new UserController(mediator);

        var result = await controller.Update(user.Id, updateUserDto, CancellationToken.None);

        var ok = result as OkObjectResult;

        Assert.NotNull(ok);
        Assert.Equal(200, ok.StatusCode);
        Assert.Equal(user.FirstName, ((UserDto)ok.Value!).FirstName);
        Assert.Equal(user.LastName, ((UserDto)ok.Value!).LastName);
        Assert.Equal(user.Age, ((UserDto)ok.Value!).Age);
    }

    [Fact]
    public async Task Should_Return_Valid_When_User_Removed()
    {
        const int userId = 1; 

        var mediator = Substitute.For<IMediator>();

        mediator
           .Send(Arg.Is<DeleteUserRequest>(r => r.Id == userId), CancellationToken.None)
           .Returns(Task.FromResult(DeleteUserResponse.Success()));


        var controller = new UserController(mediator);

        var result = await controller.Delete(userId, CancellationToken.None);

        var noContent = result as NoContentResult;

        Assert.NotNull(noContent);
        Assert.Equal(204, noContent.StatusCode);
    }
}