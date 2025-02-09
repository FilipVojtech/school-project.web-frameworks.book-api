using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BooksContext _context;

    public BooksController(BooksContext context)
    {
        _context = context;
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        if (!_context.Books.Any())
        {
            return Ok(new List<BookDto>());
        }

        var books = await _context.Books
            .AsNoTracking()
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Select(b => new BookDto(b))
            .ToListAsync();

        return Ok(books);
    }

    // GET: api/Books/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        var book = await _context.Books
            .AsNoTracking()
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(new BookDto(book));
    }

    // PUT: api/Books/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutBook(int id, BookPutDto bookDto)
    {
        if (id != bookDto.Id)
        {
            return BadRequest();
        }

        var author = await _context.Authors.FindAsync(bookDto.AuthorId);
        if (author == null)
        {
            return NotFound("Specified Author ID could not be found.");
        }

        var publisher = await _context.Publishers.FindAsync(bookDto.PublisherId);
        if (publisher == null)
        {
            return NotFound("Specified Publisher ID could not be found.");
        }

        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        book.Title = bookDto.Title;
        book.Isbn = bookDto.Isbn;
        book.Author = author;
        book.Publisher = publisher;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!BookExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Books
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<BookDto>> PostBook(BookPutDto bookDto)
    {
        var author = await _context.Authors.FindAsync(bookDto.AuthorId);
        if (author == null)
        {
            return NotFound("Specified Author ID could not be found.");
        }

        var publisher = await _context.Publishers.FindAsync(bookDto.PublisherId);
        if (publisher == null)
        {
            return NotFound("Specified Publisher ID could not be found.");
        }

        var book = new Book
        {
            Title = bookDto.Title,
            Isbn = bookDto.Isbn,
            Author = author,
            Publisher = publisher,
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, new BookDto(book));
    }

    // DELETE: api/Books/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}