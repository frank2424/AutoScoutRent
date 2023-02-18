using AutoScoutRent.Data;
using AutoScoutRent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace AutoScoutRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public UserController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }

            return await _dbContext.Users.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
      
        [HttpPost]

        public async Task<ActionResult<User>> PostUser(string username,string password,string name, string surname)
        {
            User user = new User();
            user.Username = username;
            user.Password = password;
            user.Name = name;
            user.Surname = surname;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.User_id }, user);

        }

       [HttpPost]
       [Route("login")]
       
        public async Task<ActionResult<User>> Login(string username, string password)
        {
            var userExist = _dbContext.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();

            if(userExist == null)
            {

                return BadRequest(new { message = "Incorrect username or password" });
            }


            return Ok(new { message = "Login successful" });

        }





        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, User user)
        {
            if (id != user.User_id)
            {
                return BadRequest();
            }
            _dbContext.Entry(user).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
         private bool UserExists(int id)
        {
            return (_dbContext.Users?.Any(e => e.User_id == id)).GetValueOrDefault();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }


    }

}