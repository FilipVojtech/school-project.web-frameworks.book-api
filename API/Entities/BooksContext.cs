using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public class BooksContext(DbContextOptions<BooksContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; } = null!;

    public DbSet<Author> Authors { get; set; } = null!;

    public DbSet<Publisher> Publishers { get; set; } = null!;
}