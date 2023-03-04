using Microsoft.AspNetCore.Mvc;

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