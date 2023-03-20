using System.Security.Claims;
using API.Commons;
using API.Data;
using API.DTOs.Dashboard;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Policy = "IsUser")]
    [Route("/api/dashboard")]

    public class DashboardController: ControllerBase
    {
        private DataContext _context;
        private IMapper _mapper;
        private BusinessCommon _businessCommon;
        private UsersCommon _userCommon;
        private PropertiesCommon _propertiesCommon;

        public DashboardController(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._businessCommon = new BusinessCommon(context);
            this._userCommon = new UsersCommon(context);
            this._propertiesCommon = new PropertiesCommon(context);
        }

        [HttpGet("total")]
        public ActionResult<TotalDataDtos> GetTotalData()
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

                var businessTotal = this._businessCommon.GetTotalBusiness(id);
                var usersTotal    = this._userCommon.GetTotalUsers(id);
                var propertiesTotal = this._propertiesCommon.GetTotalProperties(id);

                return new TotalDataDtos {
                    business = businessTotal,
                    properties = propertiesTotal,
                    users = usersTotal
                };
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}