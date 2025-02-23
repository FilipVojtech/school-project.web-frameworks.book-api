using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

[Table("books")]
[Index(nameof(Isbn), IsUnique = true)]
public class Book : IEquatable<Book>
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(64)]
    [Column("title")]
    public required string Title { get; set; }

    [StringLength(17)]
    [Column("isbn")]
    public string? Isbn { get; set; } = null;

    [ForeignKey("author_id")]
    public required Author Author { get; set; }

    [ForeignKey("publisher_id")]
    public required Publisher Publisher { get; set; }

    public bool Equals(Book? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Title == other.Title
               && Isbn == other.Isbn
               && Author.Id == other.Author.Id
               && Publisher.Id == other.Publisher.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Book)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Isbn, Author, Publisher);
    }

    public static bool operator ==(Book? left, Book? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Book? left, Book? right)
    {
        return !Equals(left, right);
    }
}