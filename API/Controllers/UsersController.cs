using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            this._context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            var users = this._context.Users.ToList();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUsers(int id)
        {
            return await this._context.Users.FindAsync(id);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> LoginUsers(LoginDto data)
        {
            var result = await this._context.Users.SingleOrDefaultAsync(x => (x.UserName == data.Login) || (x.Email == data.Login));

            if(result == null) Unauthorized("Invalid Login / Password, User not found");

            using var hmac = new HMACSHA512(result.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data.Password));

            for (int i = 0; i < computedHash.Length; i++) {
                if(computedHash[i] != result.PasswordHash[i]) return Unauthorized("Invalid Login / Password, User not found");
            }

            return result;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> RegisterUsers(RegisterDto data)
        {
            if(await UserNameExist(data.Username)) return BadRequest("Username already in used");

            if(await UserEmailExist(data.Email)) return BadRequest("Email Address already in used");

            using var hmac = new HMACSHA512();

            var user = new AppUser {
                UserName = data.Username,
                PasswordHash =  hmac.ComputeHash(Encoding.UTF8.GetBytes(data.Password)),
                PasswordSalt = hmac.Key,
                FirstName = data.Firstname,
                LastName = data.Lastname,
                Email = data.Email,
                Role = 1,
                Status = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            this._context.Users.Add(user);

            await this._context.SaveChangesAsync();

            return user;
        }

        private async Task<Boolean> UserNameExist(string username)
        {
            return await this._context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }

        private async Task<Boolean> UserEmailExist(string useremail)
        {
            return await this._context.Users.AnyAsync(x => x.Email.ToLower() == useremail.ToLower());
        }
    }
}