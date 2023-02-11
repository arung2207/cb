using CowsAndBulls.Server.Data;
using CowsAndBulls.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CowsAndBulls.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameLogsController : ControllerBase
{
    private readonly CowsAndBullsServerContext _context;

    public GameLogsController(CowsAndBullsServerContext context)
    {
        _context = context;
    }

    // GET: api/GameLogs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameLog>>> GetGameLog()
    {
        if (_context.GameLog == null)
        {
            return NotFound();
        }
        return await _context.GameLog.OrderByDescending(g => g.Id).ToListAsync();
    }

    // GET: api/GameLogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GameLog>> GetGameLog(int id)
    {
        if (_context.GameLog == null)
        {
            return NotFound();
        }
        var gameLog = await _context.GameLog.FindAsync(id);

        if (gameLog == null)
        {
            return NotFound();
        }

        return gameLog;
    }

    // PUT: api/GameLogs/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGameLog(int id, GameLog gameLog)
    {
        if (id != gameLog.Id)
        {
            return BadRequest();
        }

        _context.Entry(gameLog).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GameLogExists(id))
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

    // POST: api/GameLogs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GameLog>> PostGameLog(GameLog gameLog)
    {
        if (_context.GameLog == null)
        {
            return Problem("Entity set 'CowsAndBullsServerContext.GameLog'  is null.");
        }
        _context.GameLog.Add(gameLog);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetGameLog", new { id = gameLog.Id }, gameLog);
    }

    // DELETE: api/GameLogs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGameLog(int id)
    {
        if (_context.GameLog == null)
        {
            return NotFound();
        }
        var gameLog = await _context.GameLog.FindAsync(id);
        if (gameLog == null)
        {
            return NotFound();
        }

        _context.GameLog.Remove(gameLog);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/GameLogs
    [HttpDelete]
    public async Task<IActionResult> RetainOnlySomeLogEntries()
    {
        //Purge all records except for the latest 5 records
        const int KeepCount = 5;
        var recordCount = await _context.GameLog.CountAsync();
        if (recordCount > KeepCount)
        {
            var keepIds = _context.GameLog.OrderByDescending(x => x.Id).Take(KeepCount).Select(g => g.Id);

            var delete = _context.GameLog.Where(g => !keepIds.Contains(g.Id));

            _context.GameLog.RemoveRange(delete);
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    private bool GameLogExists(int id)
    {
        return (_context.GameLog?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
