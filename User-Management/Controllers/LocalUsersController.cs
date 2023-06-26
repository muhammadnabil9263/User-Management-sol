using System.Security.Cryptography;
using Azure.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Management.Data;
using User_Management.Models;
using User_Management.Models.DTO;

namespace User_Management.Controllers
{
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
        [Route("api/users")]
        public async Task<ActionResult<IEnumerable<LocalUser>>> GetlocalUsers()
        {
            if (_context.localUsers == null)
            {
                return NotFound();
            }
            return await _context.localUsers.Include(c => c.orgnization).ToListAsync();
        }


        [HttpGet]
        [Route("api/search/{searchString}")]

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
        [HttpGet]
        [Route("api/users/{id}")]

        public async Task<ActionResult<LocalUser>> GetLocalUser(int id)
        {
            if (_context.localUsers == null)
            {
                return NotFound();
            }
            var localUser = await _context.localUsers.Include(c => c.orgnization).SingleOrDefaultAsync(a => a.Id == id);

            if (localUser == null)
            {
                return NotFound();
            }

            return localUser;
        }

        // PUT: api/LocalUsers/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("api/user/{id}")]
        public async Task<IActionResult> PutLocalUser(int id, UserEditRequest updatedUser)
        {

            var user = await _context.localUsers.SingleOrDefaultAsync(a => a.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.UserName = updatedUser.UserName;
            user.OrgnizationId = updatedUser.OrgnizationId;
            _context.Entry(user).State = EntityState.Modified;

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



        //// POST: api/LocalUsers
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPost]
        [Route("api/users")]
        public async Task<ActionResult<LocalUser>> PostLocalUser(UserRegistrationRequest request)
        {
            if (_context.localUsers.Any(u => u.Email == request.Email || u.UserName == request.UserName))
            {
                return BadRequest("User already exists.");
            }

            CreatePasswordHash(request.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            var user = new LocalUser
            {
                UserName = request.UserName,
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                OrgnizationId = request.OrgnizationId
            };
            _context.localUsers.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocalUser", new { id = user.Id }, user);
        }



        //// DELETE: api/LocalUsers/5
        [HttpDelete]
        [Route("api/users/{id}")]
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

        [HttpPut]
        [Route("api/users/{id}/reset-password")]
        public async Task<IActionResult> Resetpassword(int id, ResetPassword request)
        {
            var user = await _context.localUsers.FindAsync(id);

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("current password is wrong");
            }

            CreatePasswordHash(request.newPassword,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


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
            return Ok();

        }




        private bool LocalUserExists(int id)
        {
            return (_context.localUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));


            }


        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
