namespace Tests.User.Application.Mappers;

public static class UserMapper
{
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