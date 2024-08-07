using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Data;
using UserManagementApi.Models;
using System.Linq;

namespace UserManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Invalid login request.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }


        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid user data.");
            }

            if (_context.Users.Any(u => u.Username == newUser.Username))
            {
                return Conflict("Username already exists.");
            }

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return CreatedAtAction(nameof(CreateUser), new { id = newUser.Id }, newUser);
        }

        [HttpPost("forgetpassword")]
        public IActionResult ForgetPassword([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid email.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Logic to handle password recovery (e.g., sending an email)
            return Ok("Password recovery instructions sent.");
        }
    }
}
