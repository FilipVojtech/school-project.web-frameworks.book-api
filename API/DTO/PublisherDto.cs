using API.Entities;

namespace API.DTO;

/// <remarks>
///     When an expression property is introduced into the <see cref="API.Entities.Publisher"/> entity
///     and those values are used within the <see cref="API.DTO.PublisherDto"/>,
///     create a new Publisher PUT DTO without those properties.
/// </remarks>
public class PublisherDto(Publisher publisher)
{
    public int Id { get; set; } = publisher.Id;

    public string Name { get; set; } = publisher.Name;

    public string? Url { get; set; } = publisher.Url;
}