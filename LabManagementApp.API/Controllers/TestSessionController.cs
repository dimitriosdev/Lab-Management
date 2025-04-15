using LabManagementApp.Infrastructure.Contexts;
using LabManagementApp.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagementApp.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TestSessionController : ControllerBase
  {
    private readonly LabManagementDbContext _context;

    public TestSessionController(LabManagementDbContext context)
    {
      _context = context;
    }

    // GET: api/TestSession
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TestSession>>> GetTestSessions()
    {
      return await _context.TestSessions.ToListAsync();
    }

    // GET: api/TestSession/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TestSession>> GetTestSession(Guid id)
    {
      var testSession = await _context.TestSessions.FindAsync(id);

      if (testSession == null)
      {
        return NotFound();
      }

      return testSession;
    }

    // POST: api/TestSession
    [HttpPost]
    public async Task<ActionResult<TestSession>> PostTestSession(TestSession testSession)
    {
      _context.TestSessions.Add(testSession);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetTestSession), new { id = testSession.TestSessionID }, testSession);
    }

    // PUT: api/TestSession/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTestSession(Guid id, TestSession testSession)
    {
      if (id != testSession.TestSessionID)
      {
        return BadRequest();
      }

      _context.Entry(testSession).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TestSessionExists(id))
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

    // DELETE: api/TestSession/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTestSession(Guid id)
    {
      var testSession = await _context.TestSessions.FindAsync(id);
      if (testSession == null)
      {
        return NotFound();
      }

      _context.TestSessions.Remove(testSession);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool TestSessionExists(Guid id)
    {
      return _context.TestSessions.Any(e => e.TestSessionID == id);
    }
  }
}