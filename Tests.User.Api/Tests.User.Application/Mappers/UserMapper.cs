using Tests.User.Application.DTOs.User;

namespace Tests.User.Application.Mappers;

public static class UserMapper
{
    public static Domain.Entities.User ToEntity(this CreateUserDto createUserDto)
    {
        return new Domain.Entities.User
               {
                   FirstName = createUserDto.FirstName,
                   LastName = createUserDto.LastName,
                   Age = createUserDto.Age!.Value
               };
    }

    public static UserDto ToDto(this Domain.Entities.User user)
    {
        return new UserDto
               {
                   Id = user.Id,
                   FirstName = user.FirstName,
                   LastName = user.LastName,
                   Age = user.Age
               };
    }
}