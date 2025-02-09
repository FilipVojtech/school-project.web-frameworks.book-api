using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly BooksContext _context;

    public AuthorsController(BooksContext context)
    {
        _context = context;
    }

    private bool AuthorExists(int id)
    {
        return _context.Authors.Any(e => e.Id == id);
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(b => b.Id == id);
    }

    #region Author Actions

    // GET: api/Authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
    {
        if (!_context.Authors.Any())
        {
            return NotFound();
        }

        var authors = await _context.Authors
            .Select(a => new AuthorDto(a))
            .ToListAsync();

        return Ok(authors);
    }

    // GET: api/Authors/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return new AuthorDto(author);
    }

    // PUT: api/Authors/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAuthor(int id, Author author)
    {
        if (id != author.Id)
        {
            return BadRequest();
        }

        _context.Entry(author).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AuthorExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Authors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AuthorDto>> PostAuthor(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, new AuthorDto(author));
    }

    // DELETE: api/Authors/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    #endregion
    
    #region Author Book Actions

    // GET: /api/Authors/5/books
    [HttpGet("{id:int}/books")]
    public async Task<ActionResult<AuthorBooksDto>> GetAuthorBooks(int id)
    {
        if (!AuthorExists(id))
        {
            return NotFound();
        }

        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

        return Ok(new AuthorBooksDto(author!));
    }

    // POST: /api/Authors/5/books
    [HttpPost("{id:int}/books")]
    public async Task<IActionResult> AddBookToAuthor(int id, int bookId)
    {
        if (!AuthorExists(id))
        {
            return NotFound();
        }

        if (!BookExists(id))
        {
            return BadRequest("Specified Book ID could not be found.");
        }

        var book = _context.Books.FirstOrDefault(b => b.Id == bookId)!;
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);

        author!.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAuthorBooks), new { id = author.Id }, new AuthorBooksDto(author));
    }

    [HttpDelete("{id:int}/books")]
    public async Task<IActionResult> RemoveBookFromAuthor(int id, int bookId)
    {
        if (!AuthorExists(id))
        {
            return NotFound();
        }

        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        var book = author!.Books.FirstOrDefault(b => b.Id == bookId);

        if (book != null)
        {
            author.Books.Remove(book);
        }

        return NoContent();
    }

    #endregion
}