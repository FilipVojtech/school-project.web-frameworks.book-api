using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTO;

public class AuthorPutDto
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public DateOnly BirthDate { get; set; }

    public DateOnly? DateOfPassing { get; set; }

    public AuthorPutDto()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public AuthorPutDto(Author author)
    {
        Id = author.Id;
        FirstName = author.FirstName;
        LastName = author.LastName;
        BirthDate = author.BirthDate;
        DateOfPassing = author.DateOfPassing ?? null;
    }
}