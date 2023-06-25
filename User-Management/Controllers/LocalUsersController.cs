using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Management.Data;
using User_Management.Models;

namespace User_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LocalUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LocalUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LocalUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocalUser>>> GetlocalUsers()
        {
          if (_context.localUsers == null)
          {
              return NotFound();
          }
            return await _context.localUsers.Include(c => c.orgnization).ToListAsync();
        }


        [HttpGet("search/{searchString}")]
        public ActionResult<IEnumerable<LocalUser>> SearchlocalUsers(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var query = _context.localUsers.Include(c => c.orgnization).Where(s => s.Name.Contains(searchString)
                || s.Email.Contains(searchString)).ToList();
                return query;
            }
            return NotFound();
        }





        // GET: api/LocalUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocalUser>> GetLocalUser(int id)
        {
          if (_context.localUsers == null)
          {
              return NotFound();
          }
            var localUser = await _context.localUsers.Include(c => c.orgnization).SingleOrDefaultAsync(a=> a.Id==id);

            if (localUser == null)
            {
                return NotFound();
            }

            return localUser;
        }

        // PUT: api/LocalUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocalUser(int id, LocalUser localUser)
        {
            if (id != localUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(localUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalUserExists(id))
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



        // POST: api/LocalUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        
        
        public async Task<ActionResult<LocalUser>> PostLocalUser(LocalUser localUser)
        {
          if (_context.localUsers == null)
          {
              return Problem("Entity set 'ApplicationDbContext.localUsers'  is null.");
          }

            if (ModelState.IsValid)
            {
                _context.Add(localUser);
                await _context.SaveChangesAsync();
                return localUser ; 
            }


            return CreatedAtAction("GetLocalUser", new { id = localUser.Id }, localUser);
        }



        // DELETE: api/LocalUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocalUser(int id)
        {
            if (_context.localUsers == null)
            {
                return NotFound();
            }
            var localUser = await _context.localUsers.FindAsync(id);
            if (localUser == null)
            {
                return NotFound();
            }

            _context.localUsers.Remove(localUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocalUserExists(int id)
        {
            return (_context.localUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
