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

        public string RemoveSpecialChars(string input)
        {
            return input; //Regex.Replace(input, @"[^0-9a-zA-Z\._]", string.Empty);
        }
    }
}