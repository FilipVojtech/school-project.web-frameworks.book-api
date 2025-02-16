using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTO;

/// <remarks>
///     When an expression property is introduced into the <see cref="API.Entities.Publisher"/> entity
///     and those values are used within the <see cref="API.DTO.PublisherDto"/>,
///     create a new Publisher PUT DTO without those properties.
/// </remarks>
public class PublisherDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string? Url { get; set; }

    public PublisherDto()
    {
        Name = string.Empty;
    }

    public PublisherDto(Publisher publisher, string name)
    {
        Id = publisher.Id;
        Name = publisher.Name;
        Url = publisher.Url;
    }
}