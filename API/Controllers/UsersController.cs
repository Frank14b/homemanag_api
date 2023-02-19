using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //api/users
    public class UsersController
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

        // [HttpPost]
        // public async Task<ActionResult<AppUser>> AddUsers()
        // {
        //     var result = await this._context.Users.AddAsync();

        //     return result;
        // }
    }
}