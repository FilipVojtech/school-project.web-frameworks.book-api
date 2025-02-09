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
            .Select(p => new PublisherDto(p))
            .ToListAsync();

        return Ok(publishers);
    }

    // GET: api/Publishers/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PublisherDto>> GetPublisher(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);

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
    public async Task<ActionResult<AuthorBooksDto>> GetAuthorBooks(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);

        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(new PublisherBooksDto(publisher));
    }

    #endregion
}