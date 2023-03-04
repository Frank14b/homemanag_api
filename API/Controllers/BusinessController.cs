using System.Security.Claims;
using API.Commons;
using API.Data;
using API.DTOs.Business;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class BusinessController: BaseApiController
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private BusinessCommon _businessCommon;
        private UsersCommon _userCommon;

        public BusinessController(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._businessCommon = new BusinessCommon(context);
            this._userCommon = new UsersCommon(context);
        }

        [HttpPost("add")]
        public async Task<ActionResult<BusinessResultDtos>> CreateBusiness(CreateBusinessDto data)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var userid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

                var _business = this._mapper.Map<AppBusiness>(data);
                _business.Reference = _business.Name.GetHashCode()+"_"+userid;
                _business.UserId = userid;
                _business.Status = (int)StatusEnum.enable;
                _business.CreatedAt = DateTime.UtcNow;

                if (await this._businessCommon.BusinessExist(_business, userid)) return  BadRequest("Business Name already in used");

                this._context.Business.Add(_business);

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<BusinessResultDtos>(_business);

                return result;
            }
            catch (System.Exception ex)
            {
                return BadRequest("Business can't be created " + ex);
            }
        }
        
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<BusinessResultDtos>>> GetAllBusiness()
        {
            try
            {
                var _business = await this._context.Business.Where(x => x.Status != (int)StatusEnum.delete).Include(p => p.User).ToListAsync();
                var result = this._mapper.Map<IEnumerable<BusinessResultDtos>>(_business);

                return Ok(result);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("{businessid}")]
        public async Task<ActionResult<BusinessResultDtos>> GetOneBusiness(int businessid)
        {
            try
            {
                var _business = await this._context.Business.Where(x => x.Id == businessid && x.Status == (int)StatusEnum.enable).Include(p => p.User).FirstOrDefaultAsync();
                if(_business == null) return NotFound("Business not Found or invalid ID");

                var result = this._mapper.Map<BusinessResultDtos>(_business);

                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured or business not found");
            }
        }

        [HttpGet("getbyuser/{userid}")]
        public ActionResult<IEnumerable<BusinessResultDtos>> GetByUser(int userid)
        {
            try
            {
                var _business = this._context.Business.Where(x => x.UserId == userid && x.User.Status == (int)StatusEnum.enable && x.Status == (int)StatusEnum.enable).ToList();
                if(_business == null) return NotFound("Business not Found or invalid ID");

                var result = this._mapper.Map<IEnumerable<BusinessResultDtos>>(_business);
                
                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured or business not found");
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteBusinessResultDto>> DeleteBusiness(DeleteBusinessDto data)
        {
            try
            {
                var _business = await this._context.Business.Where(x => x.Id == data.Id && x.Status == (int)StatusEnum.enable).FirstOrDefaultAsync();
                _business.Status = (int)StatusEnum.delete;
                await this._context.SaveChangesAsync();

                return new DeleteBusinessResultDto
                {
                    Status = true,
                    Message = "The giving Business has been deleted"
                };
            }
            catch (System.Exception)
            {
                return new DeleteBusinessResultDto
                {
                    Status = false,
                    Message = "The giving Business counldn't been deleted"
                };
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<BusinessResultDtos>> UpdateBusiness(UpdateBusinessDto data)
        {
            try
            {
                var _business = await this._context.Business.Where(x => x.Id == data.Id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();

                if(_business == null) return NotFound("Business not Found");

                this._mapper.Map<AppBusiness>(data);

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<BusinessResultDtos>(_business);

                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("The giving business couldn't be edited");
            }
        }
    }
}