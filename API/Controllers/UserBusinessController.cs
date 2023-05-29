using System.Security.Claims;
using API.Commons;
using API.Data;
using API.DTOs.UserBusiness;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize(Policy = "IsUser")]
    [Route("/api/user_business")]
    public class UserBusinessController : BaseApiController
    {
        private readonly ILogger<UserBusinessController> _logger;
        private readonly DataContext _context;
        private IMapper _mapper;
        private UserBusinessCommon _userBusinessCommon;

        public UserBusinessController(
            ILogger<UserBusinessController> logger,
            DataContext context,
            IMapper mapper
        )
        {
            this._logger = logger;
            this._context = context;
            this._mapper = mapper;
            this._userBusinessCommon = new UserBusinessCommon(context);
        }

        [HttpGet("")]
        public async Task<ActionResult<UserBusinessResultDto>> GetAll(int skip = 0, int limit = 0, string sort = "asc", int userId = 0, int businesId = 0)
        {
            try
            {
                var data = await this._context.UserBusiness.Where((x) => x.Status != (int)StatusEnum.delete).ToListAsync();

                var finalresult = this._mapper.Map<UserBusinessResultDto>(data);

                return finalresult;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserBusinessResultDto>> GetOne(int id)
        {
            try
            {
                var data = await this._context.UserBusiness.Where((x) => x.Status != (int)StatusEnum.delete && x.Id == id).FirstOrDefaultAsync();

                var finalresult = this._mapper.Map<UserBusinessResultDto>(data);

                return finalresult;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<UserBusinessResultDto>> AddNew(UserBusinessDto data) 
        {
            try
            {
                var checkExist = await this._context.UserBusiness.SingleOrDefaultAsync(x => ((x.BusinesId == data.BusinesId) || (x.UserId == data.UserId)) && x.Status != (int)StatusEnum.delete);

                if (checkExist != null) BadRequest("These user data's already exists");

                var _data = this._mapper.Map<AppUserBusines>(data);

                this._context.UserBusiness.Add(_data);

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<UserBusinessResultDto>(_data);

                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserBusinessDeleteDto>> Delete(int id) 
        {
            try
            {
                if (await this._userBusinessCommon.UserBusinessExist(id)) return BadRequest("User Business not found");

                var user = await this._context.UserBusiness.Where(x => x.Id == id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();
                user.Status = (int)StatusEnum.delete;
                await this._context.SaveChangesAsync();

                return new UserBusinessDeleteDto
                {
                    Status = true,
                    Message = "User business link has been deleted"
                };
            }
            catch (System.Exception)
            {
                return new UserBusinessDeleteDto
                {
                    Status = false,
                    Message = "An Error Occured or User business link not found"
                };
            }
        }
    }
}