using System.Security.Claims;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //api/routes

    public class BaseApiController : ControllerBase
    {
        public int connected_user_id;

        public AppUser connected_user_data;

        private DataContext _context;

        public BaseApiController()
        {
            try
            {
                var userIdentity = (User.Identity as ClaimsIdentity);
                if (userIdentity != null)
                {
                    var userId = Int32.Parse(userIdentity.Claims.FirstOrDefault().Value); // get user id from the auth token

                    if ((int)userId > 0)
                    {
                        this.connected_user_id = userId;

                        this.connected_user_data = this._context.Users.Where(x => (x.Id == userId) && x.Status == (int)StatusEnum.enable).FirstOrDefault();
                    }
                }
            }
            catch (System.Exception)
            {}
        }
    }
}