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

    // GET: api/Publishers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
    {
        return await _context.Publishers.ToListAsync();
    }

    // GET: api/Publishers/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Publisher>> GetPublisher(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);

        if (publisher == null)
        {
            return NotFound();
        }

        return publisher;
    }

    // PUT: api/Publishers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
    {
        if (id != publisher.Id)
        {
            return BadRequest();
        }

        _context.Entry(publisher).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PublisherExists(id))
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

    // POST: api/Publishers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
    {
        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPublisher), new { id = publisher.Id }, publisher);
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
}