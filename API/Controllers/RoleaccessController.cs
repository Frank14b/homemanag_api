
using API.Data;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class RoleaccessController: BaseApiController
    {
        private readonly DataContext _context;
        public RoleaccessController(DataContext context)
        {
            this._context = context;
        }
    }
}