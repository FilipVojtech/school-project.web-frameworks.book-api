using API.Entities;

namespace API.DTO;

public class BookDto(Book book)
{
    public int Id { get; set; } = book.Id;

    public string Title { get; set; } = book.Title;

    public string? Isbn { get; set; } = book.Isbn;

    public AuthorDto Author { get; set; } = new AuthorDto(book.Author);

    public PublisherDto Publisher { get; set; } = new PublisherDto(book.Publisher);
}