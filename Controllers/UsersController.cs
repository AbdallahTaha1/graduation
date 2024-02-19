using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using graduation.Models;
using graduation.Models.Dto;

namespace graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
          return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();        

            return NoContent();
        }

        [Route("CreateStudent")]
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> CreateStudent([FromForm]StudentDto studentDto)
        {
            var student = new User {
                UserId = studentDto.Id,
                Name = studentDto.Name,
                Password = studentDto.Password,
                Role = Roles.Student
            };
            _context.Users.Add(student);
            
            await _context.SaveChangesAsync();
            
            return Ok(CreatedAtAction("CreateStudent", new { id = studentDto.Id }, studentDto));
        }

        [Route("CreateDoctor")]
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> CreateDoctor([FromForm] DoctorDto doctorDto)
        {
            var doctor = new User
            {
                UserId = doctorDto.Id,
                Name = doctorDto.Name,
                Password = doctorDto.Password,
                Role = Roles.Doctor
            };
            _context.Users.Add(doctor);

            await _context.SaveChangesAsync();

            return Ok(CreatedAtAction("CreateStudent", new { id = doctorDto.Id }, doctorDto));
        }

        [Route("createAdmin")]
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> CreateAdmin([FromForm] AdminDto adminDto)
        {
            var admin = new User
            {
                UserId = adminDto.Id,
                Name = adminDto.Name,
                Password = adminDto.Password,
                Role = Roles.Admin
            };
            _context.Users.Add(admin);

            await _context.SaveChangesAsync();

            return Ok(CreatedAtAction("CreateStudent", new { id = adminDto.Id }, adminDto));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       
    }
}
