﻿using API.Entities;

namespace API.DTO;

public class PublisherBooksDto(Publisher publisher)
{
    public int Id { get; set; } = publisher.Id;

    public string Name { get; set; } = publisher.Name;
    
    public ICollection<PublisherBooksBookDto> Books { get; set; } = publisher.Books
        .Select(b => new PublisherBooksBookDto(b))
        .ToList();

    public class PublisherBooksBookDto(Book book)
    {
        public int Id { get; set; } = book.Id;

        public string Title { get; set; } = book.Title;

        public string? Isbn { get; set; } = book.Isbn;

        public AuthorDto Author { get; set; } = new AuthorDto(book.Author);
    }
}