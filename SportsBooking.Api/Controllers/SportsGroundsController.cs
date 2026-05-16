using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsBooking.Data;
using SportsBooking.Models;

namespace SportsBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SportsGroundsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SportsGroundsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SportsGround>>> GetAll()
    {
        return await _context.SportsGrounds
            .Include(g => g.GroundType)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SportsGround>> GetById(int id)
    {
        var ground = await _context.SportsGrounds
            .Include(g => g.GroundType)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (ground == null)
            return NotFound();

        return ground;
    }

    [HttpPost]
    public async Task<ActionResult<SportsGround>> Create(SportsGround ground)
    {
        ground.GroundType = null!;

        _context.SportsGrounds.Add(ground);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = ground.Id }, ground);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SportsGround ground)
    {
        if (id != ground.Id)
            return BadRequest();

        _context.Entry(ground).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ground = await _context.SportsGrounds.FindAsync(id);

        if (ground == null)
            return NotFound();

        _context.SportsGrounds.Remove(ground);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
