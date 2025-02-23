using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTO;

public class BookPutDto
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string? Isbn { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [Required]
    public int PublisherId { get; set; }

    public BookPutDto()
    {
        Title = "";
    }

    public BookPutDto(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Isbn = book.Isbn;
        AuthorId = book.Author.Id;
        PublisherId = book.Publisher.Id;
    }
}