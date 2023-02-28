
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authentication;
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

        public Task<ResultContext<AppAcces>> CreateAccess()
        {
            
        }
    }
}