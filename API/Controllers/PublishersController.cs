using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublishersController : ControllerBase
{
    private readonly BooksContext _context;

    public PublishersController(BooksContext context)
    {
        _context = context;
    }

    private bool PublisherExists(int id)
    {
        return _context.Publishers.Any(e => e.Id == id);
    }

    #region Publisher Actions

    // GET: api/Publishers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PublisherDto>>> GetPublishers()
    {
        if (!_context.Publishers.Any())
        {
            return Ok(new List<PublisherDto>());
        }

        var publishers = await _context.Publishers
            .AsNoTracking()
            .Select(p => new PublisherDto(p))
            .ToListAsync();

        return Ok(publishers);
    }

    // GET: api/Publishers/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PublisherDto>> GetPublisher(int id)
    {
        var publisher = await _context.Publishers
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(new PublisherDto(publisher));
    }

    // PUT: api/Publishers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutPublisher(int id, PublisherDto publisherDto)
    {
        if (id != publisherDto.Id)
        {
            return BadRequest();
        }

        var publisher = await _context.Publishers.FindAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }

        publisher.Name = publisherDto.Name;
        publisher.Url = publisherDto.Url;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!PublisherExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Publishers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<PublisherDto>> PostPublisher(PublisherDto publisherDto)
    {
        var publisher = new Publisher
        {
            Name = publisherDto.Name,
            Url = publisherDto.Url,
        };

        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPublisher), new { id = publisher.Id }, new PublisherDto(publisher));
    }

    // DELETE: api/Publishers/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }

        _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    #endregion

    #region Publisher Book Actions

    // GET: /api/Publishers/5/books
    [HttpGet("{id:int}/books")]
    public async Task<ActionResult<PublisherBooksDto>> GetPublisherBooks(int id)
    {
        var publisher = await _context.Publishers
            .AsNoTracking()
            .Include(p => p.Books)
            .ThenInclude(b => b.Author)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(new PublisherBooksDto(publisher));
    }

    // POST: /api/Publishers/5/books
    [HttpPost("{id:int}/books")]
    public async Task<IActionResult> AddBookToPublisher(int id, int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        var publisher = await _context.Publishers.FindAsync(id);

        if (publisher == null)
        {
            return NotFound();
        }

        if (book == null)
        {
            return BadRequest("Specified Book ID could not be found.");
        }

        publisher.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPublisherBooks), new { id = publisher.Id }, new PublisherBooksDto(publisher));
    }

    // DELETE: /api/Publishers/5/books
    [HttpDelete("{id:int}/books")]
    public async Task<IActionResult> RemoveBookFromPublisher(int id, int bookId)
    {
        var publisher = await _context.Publishers.FindAsync(id);

        if (publisher == null)
        {
            return NotFound();
        }

        var book = publisher.Books.FirstOrDefault(b => b.Id == bookId);

        if (book != null)
        {
            publisher.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        else
        {
            return NotFound("Book not found for this author");
        }

        return NoContent();
    }

    #endregion
}