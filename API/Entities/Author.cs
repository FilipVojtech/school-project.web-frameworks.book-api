using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

[Table("authors")]
[Index(nameof(FirstName), nameof(LastName), Name = "Author_Unique_Full_Name", IsUnique = true)]
public class Author : IEquatable<Author>
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(32)]
    [Column("first_name")]
    public required string FirstName { get; set; }

    [StringLength(32)]
    [Column("last_name")]
    public required string LastName { get; set; }

    [Column("birth_date")]
    public required DateOnly BirthDate { get; set; }

    [Column("date_of_passing")]
    public DateOnly? DateOfPassing { get; set; }

    public virtual ICollection<Book> Books { get; } = [];

    public string FullName => $"{FirstName} {LastName}";

    public int Age => (DateOfPassing?.Year ?? DateTime.Now.Year) - BirthDate.Year;

    public bool Equals(Author? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return FirstName == other.FirstName
               && LastName == other.LastName
               && BirthDate.Equals(other.BirthDate)
               && Nullable.Equals(DateOfPassing, other.DateOfPassing);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Author)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName, BirthDate, DateOfPassing);
    }

    public static bool operator ==(Author? left, Author? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Author? left, Author? right)
    {
        return !Equals(left, right);
    }
}