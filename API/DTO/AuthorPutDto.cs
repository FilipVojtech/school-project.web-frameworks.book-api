using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTO;

public class AuthorPutDto(Author author)
{
    public int Id { get; set; } = author.Id;

    [Required]
    public string FirstName { get; set; } = author.FirstName;

    [Required]
    public string LastName { get; set; } = author.LastName;

    [Required]
    public DateOnly BirthDate { get; set; } = author.BirthDate;

    public DateOnly? DateOfPassing { get; set; } = author.DateOfPassing ?? null;
}