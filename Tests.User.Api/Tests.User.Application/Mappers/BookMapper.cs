namespace Tests.User.Application.Mappers;

public static class BookMapper
{
    public static Book ToEntity(this CreateBookDto createBookDto)
    {
        return new Book
               {
                   Title = createBookDto.Title,
                   Author = createBookDto.Author
               };
    }

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