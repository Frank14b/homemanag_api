
using API.Data;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class AccessController: BaseApiController
    {
        private readonly DataContext _context;
        public AccessController(DataContext context)
        {
            this._context = context;
        }
    }
}