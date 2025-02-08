using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("authors")]
public class Author
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
}