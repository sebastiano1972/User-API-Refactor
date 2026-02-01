namespace Tests.User.Application.Mappers;

public static class BookMapper
{
    extension(Book book)
    {
        public BookDto ToDto()
        {
            return new BookDto
                   {
                       Id = book.Id,
                       Title = book.Title,
                       Author = book.Author,
                       Comments = book.Comments.Select(c=>c.ToDto()).ToList()
                   };
        }

        public BookListDto ToListDto()
        {
            return new BookListDto
                   {
                       Id = book.Id,
                       Title = book.Title,
                       Author = book.Author
                   };
        }
    }
}