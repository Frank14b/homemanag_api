
using API.Commons;
using API.Data;
using API.DTOs.Roles;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [Authorize]
    public class RolesController : BaseApiController
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private RolesCommon _rolesCommon;
        private BusinessCommon _businessCommon;

        public RolesController(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._rolesCommon = new RolesCommon(context);
            this._businessCommon = new BusinessCommon(context);
        }

        [HttpPost("add")]
        public async Task<ActionResult<RoleResultDtos>> CreateRoles(CreateRolesDto data)
        {
            try
            {
                if (await this._rolesCommon.RoleExist(data.Title, data.BusinessId, 0)) return BadRequest("Roles Name already in used");

                if(!await this._businessCommon.BusinessIdExist(data.BusinessId)) return NotFound("Please Provide a Valid Business ID");

                var _role = this._mapper.Map<AppRole>(data);

                _role.Code = data.Title.GetHashCode()+"$"+data.BusinessId;
                _role.Business = this._businessCommon.GetBusinessById(data.BusinessId);
                _role.Status = (int)StatusEnum.enable;

                this._context.Roles.Add(_role);

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<RoleResultDtos>(_role);

                return result;
            }
            catch (System.Exception ex)
            {
                return BadRequest("Role can't be created " + ex);
            }
        }
        
        [HttpGet("getall/{businessid}")]
        public async Task<ActionResult<IEnumerable<AppRole>>> GetAllRoles(int businessid)
        {
            try
            {
                var _role = await this._context.Roles.Where(x => x.Status != (int)StatusEnum.delete && x.BusinessId == businessid).ToListAsync();
                return _role;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("{roleid}")]
        public async Task<ActionResult<RoleResultDtos>> GetOneRoles(int roleid)
        {
            try
            {
                var _role = await this._context.Roles.Where(x => x.Id == roleid && x.Status == (int)StatusEnum.enable).FirstOrDefaultAsync();
                var result = this._mapper.Map<RoleResultDtos>(_role);
                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured or access not found");
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteRoleResultDto>> DeleteRoles(DeleteRolesDto data)
        {
            try
            {
                var _access = await this._context.Roles.Where(x => x.Id == data.Id && x.Status == (int)StatusEnum.enable).FirstOrDefaultAsync();
                _access.Status = (int)StatusEnum.delete;
                await this._context.SaveChangesAsync();

                return new DeleteRoleResultDto
                {
                    Status = true,
                    Message = "The giving Roles has been deleted"
                };
            }
            catch (System.Exception)
            {
                return new DeleteRoleResultDto
                {
                    Status = false,
                    Message = "The giving Roles counldn't been deleted"
                };
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<RoleResultDtos>> UpdateRoles(UpdateRolesDto data)
        {
            try
            {
                var _role = await this._context.Roles.Where(x => x.Id == data.Id && x.Status != (int)StatusEnum.delete).Include("Busines").FirstOrDefaultAsync();

                _role.Title = (data.Title != null) ? data.Title : _role.Title;
                _role.Description = (data.Description != null) ? data.Description : _role.Description;
                _role.Status = (int)StatusEnum.enable;

                if (await this._rolesCommon.RoleExist(_role.Title, 1, _role.Id)) return BadRequest("Roles Name already in used");

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<RoleResultDtos>(_role);

                return result;
            }
            catch (System.Exception ex)
            {
                return BadRequest("The giving role couldn't be edited" + ex);
            }
        }
    }
}