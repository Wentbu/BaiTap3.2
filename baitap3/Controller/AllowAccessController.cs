// Controllers/AllowAccessController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaiOnline3.Data;
using BaiOnline3.Models;
using Microsoft.AspNetCore.Authorization;

namespace BaiOnline3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AllowAccessController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AllowAccessController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllowAccess>>> GetAllowAccesses()
        {
            return await _context.AllowAccesses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AllowAccess>> GetAllowAccess(int id)
        {
            var allowAccess = await _context.AllowAccesses.FindAsync(id);
            if (allowAccess == null) return NotFound();
            return allowAccess;
        }

        [HttpPost]
        public async Task<ActionResult<AllowAccess>> CreateAllowAccess(AllowAccess allowAccess)
        {
            _context.AllowAccesses.Add(allowAccess);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllowAccess), new { id = allowAccess.Id }, allowAccess);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAllowAccess(int id, AllowAccess allowAccess)
        {
            if (id != allowAccess.Id) return BadRequest();
            _context.Entry(allowAccess).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllowAccess(int id)
        {
            var allowAccess = await _context.AllowAccesses.FindAsync(id);
            if (allowAccess == null) return NotFound();

            _context.AllowAccesses.Remove(allowAccess);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}