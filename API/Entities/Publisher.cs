using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

[Table("publishers")]
[Index(nameof(Name), IsUnique = true)]
public class Publisher : IEquatable<Publisher>
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(64)]
    [Column("name")]
    public required string Name { get; set; }

    [DataType(DataType.Url)]
    [StringLength(256)]
    [Column("url")]
    public string? Url { get; set; }

    public virtual ICollection<Book> Books { get; } = [];

    public bool Equals(Publisher? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name
               && Url == other.Url;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Publisher)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Url);
    }

    public static bool operator ==(Publisher? left, Publisher? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Publisher? left, Publisher? right)
    {
        return !Equals(left, right);
    }
}