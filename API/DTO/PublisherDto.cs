using API.Entities;

namespace API.DTO;

public class PublisherDto(Publisher publisher)
{
    public int Id { get; set; } = publisher.Id;

    public string Name { get; set; } = publisher.Name;

    public string? Url { get; set; } = publisher.Url;
}