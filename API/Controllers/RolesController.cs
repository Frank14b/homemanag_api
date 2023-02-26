
using API.Data;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class RolesController : BaseApiController
    {
        private readonly DataContext _context;
        public RolesController(DataContext context) 
        {
            this._context = context;
        }
    }
}