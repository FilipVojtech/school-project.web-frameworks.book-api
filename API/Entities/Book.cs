using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

[Table("books")]
[Index(nameof(Isbn), IsUnique = true)]
public class Book
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
}