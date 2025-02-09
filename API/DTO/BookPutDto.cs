using API.Entities;

namespace API.DTO;

public class BookPutDto(Book book)
{
    public int Id { get; set; } = book.Id;

    public string Title { get; set; } = book.Title;

    public string? Isbn { get; set; } = book.Isbn;

    public int AuthorId { get; set; } = book.Author.Id;

    public int PublisherId { get; set; } = book.Publisher.Id;
}