using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

[Table("publishers")]
[Index(nameof(Name), IsUnique = true)]
public class Publisher
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
}