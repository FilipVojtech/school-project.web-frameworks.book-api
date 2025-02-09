using API.Entities;

namespace API.DTO;

public class AuthorBooksDto(Author author)
{
    public int Id { get; set; } = author.Id;

    public string Name { get; set; } = author.FullName;

    public ICollection<BookDto> Books { get; set; } = author.Books
        .Select(b => new BookDto(b))
        .ToList();

    public class BookDto(Book book)
    {
        public int Id { get; set; } = book.Id;

        public string Title { get; set; } = book.Title;

        public string? Isbn { get; set; } = book.Isbn;

        public PublisherDto Publisher { get; set; } = new PublisherDto(book.Publisher);
    }
}