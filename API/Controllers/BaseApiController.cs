using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //api/routes for users
    [Route("api/admin/[controller]")] //api/routes for su-admin

    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {

            // this._context = context;
        }
    }
}