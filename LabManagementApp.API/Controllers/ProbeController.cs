using LabManagementApp.Infrastructure.Contexts;
using LabManagementApp.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LabManagementApp.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProbeController : ControllerBase
  {
    private readonly LabManagementDbContext _context;
    private readonly ILogger<ProbeController> _logger;

    public ProbeController(LabManagementDbContext context, ILogger<ProbeController> logger)
    {
      _context = context;
      _logger = logger;
    }

    // GET: api/Probe
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Probe>>> GetProbes()
    {
      _logger.LogInformation("Fetching all probes.");
      return await _context.Probes.ToListAsync();
    }

    // GET: api/Probe/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Probe>> GetProbe(Guid id)
    {
      _logger.LogInformation("Fetching probe with ID: {ProbeID}", id);
      var probe = await _context.Probes.FindAsync(id);

      if (probe == null)
      {
        _logger.LogWarning("Probe with ID: {ProbeID} not found.", id);
        return NotFound();
      }

      return probe;
    }

    // POST: api/Probe
    [HttpPost]
    public async Task<ActionResult<Probe>> PostProbe(Probe probe)
    {
      _context.Probes.Add(probe);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetProbe), new { id = probe.ProbeID }, probe);
    }

    // PUT: api/Probe/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProbe(Guid id, Probe probe)
    {
      if (id != probe.ProbeID)
      {
        return BadRequest();
      }

      _context.Entry(probe).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProbeExists(id))
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

    // DELETE: api/Probe/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProbe(Guid id)
    {
      var probe = await _context.Probes.FindAsync(id);
      if (probe == null)
      {
        return NotFound();
      }

      _context.Probes.Remove(probe);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ProbeExists(Guid id)
    {
      return _context.Probes.Any(e => e.ProbeID == id);
    }
  }
}