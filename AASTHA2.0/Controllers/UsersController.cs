using AASTHA2.Entities;
using AASTHA2.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AASTHA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class UsersController : ControllerBase
    {
        public IConfiguration _configuration { get; }
        private IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            _unitOfWork.Save();
            return _unitOfWork.User.FindAll();
        }

        // GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}

        //// PUT: api/Users/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Users
        [HttpPost]
        public ActionResult PostUser(User user)
        {
            _unitOfWork.User.Create(user);
            _unitOfWork.Save();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<User>> DeleteUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return user;
        //}

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
