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
}