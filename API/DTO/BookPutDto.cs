using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTO;

public class BookPutDto(Book book)
{
    public int Id { get; set; } = book.Id;

    [Required]
    public string Title { get; set; } = book.Title;

    [Required]
    public string? Isbn { get; set; } = book.Isbn;

    [Required]
    public int AuthorId { get; set; } = book.Author.Id;

    [Required]
    public int PublisherId { get; set; } = book.Publisher.Id;
}