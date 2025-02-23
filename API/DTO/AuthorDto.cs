using API.Entities;

namespace API.DTO;

public class AuthorDto(Author author)
{
    public int Id { get; set; } = author.Id;

    public string FirstName { get; set; } = author.FirstName;

    public string LastName { get; set; } = author.LastName;

    public DateOnly BirthDate { get; set; } = author.BirthDate;

    public DateOnly? DateOfPassing { get; set; } = author.DateOfPassing ?? null;

    public int Age { get; set; } = author.Age;
}