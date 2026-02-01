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

    extension(Domain.Entities.User user)
    {
        public UserDto ToDto()
        {
            return new UserDto
                   {
                       Id = user.Id,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       Age = user.Age,
                       BorrowedBooks = user.BorrowedBooks.Select(bb => bb.ToListDto()).ToList()
                   };
        }

        public UserListDto ToListDto()
        {
            return new UserListDto
                   {
                       Id = user.Id,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       Age = user.Age
                   };
        }
    }
}