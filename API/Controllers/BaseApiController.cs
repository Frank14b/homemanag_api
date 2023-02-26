using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //api/routes

    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {
            
        }
    }
}