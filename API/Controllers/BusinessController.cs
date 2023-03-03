
using API.Commons;
using API.Data;
using API.DTOs.Business;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BusinessController: BaseApiController
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private BusinessCommon _businessCommon;

        public BusinessController(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._businessCommon = new BusinessCommon(context);
        }

        [HttpPost("add")]
        public async Task<ActionResult<BusinessResultDtos>> CreateBusiness(CreateBusinessDto data)
        {
            try
            {
                var userid = this.connected_user_id;

                if (!await this._businessCommon.BusinessExist(data, userid)) return  BadRequest("Business Name already in used" + userid);

                data.UserId = userid;

                var _business = this._mapper.Map<AppBusiness>(data);

                _business.Reference = _business.Name.GetHashCode()+"_";
                // _business.User = this.connected_user_data;

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
        public ActionResult<IEnumerable<AppBusiness>> GetAllBusiness(int businessid)
        {
            try
            {
                var _business = this._context.Business.Where(x => x.Status != (int)StatusEnum.delete).ToList();
                // var result = this._mapper.Map<BusinessListResultDto>(_access);
                return _business;
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
                var _business = await this._context.Business.Where(x => x.Id == businessid && x.Status == (int)StatusEnum.enable).FirstOrDefaultAsync();
                var result = this._mapper.Map<BusinessResultDtos>(_business);
                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured or access not found");
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteBusinessResultDto>> DeleteBusiness(DeleteBusinessDto data)
        {
            try
            {
                var _access = await this._context.Business.Where(x => x.Id == data.Id && x.Status == (int)StatusEnum.enable).FirstOrDefaultAsync();
                _access.Status = (int)StatusEnum.delete;
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
                var _access = await this._context.Business.Where(x => x.Id == data.Id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();

                this._mapper.Map<AppBusiness>(data);

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<BusinessResultDtos>(_access);

                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("The giving business couldn't be edited");
            }
        }
    }
}