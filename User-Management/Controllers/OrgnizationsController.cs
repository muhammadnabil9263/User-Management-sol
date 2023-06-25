using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Management.Data;
using User_Management.Models;

namespace User_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgnizationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrgnizationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orgnizations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orgnization>>> GetOrgnizations()
        {
          if (_context.Orgnizations == null)
          {
              return NotFound();
          }
            return await _context.Orgnizations.ToListAsync();
        }

        // GET: api/Orgnizations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orgnization>> GetOrgnization(int id)
        {
          if (_context.Orgnizations == null)
          {
              return NotFound();
          }
            var orgnization = await _context.Orgnizations.FindAsync(id);

            if (orgnization == null)
            {
                return NotFound();
            }

            return orgnization;
        }

        // PUT: api/Orgnizations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrgnization(int id, Orgnization orgnization)
        {
            if (id != orgnization.Id)
            {
                return BadRequest();
            }

            _context.Entry(orgnization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrgnizationExists(id))
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

        // POST: api/Orgnizations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orgnization>> PostOrgnization(Orgnization orgnization)
        {
          if (_context.Orgnizations == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Orgnizations'  is null.");
          }
            _context.Orgnizations.Add(orgnization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrgnization", new { id = orgnization.Id }, orgnization);
        }


        // DELETE: api/Orgnizations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrgnization(int id)
        {
            if (_context.Orgnizations == null)
            {
                return NotFound();
            }
            var orgnization = await _context.Orgnizations.FindAsync(id);
            if (orgnization == null)
            {
                return NotFound();
            }

            _context.Orgnizations.Remove(orgnization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrgnizationExists(int id)
        {
            return (_context.Orgnizations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
