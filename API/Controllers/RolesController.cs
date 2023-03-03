
using API.Commons;
using API.Data;
using API.DTOs.Roles;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        public RolesController(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._rolesCommon = new RolesCommon(context);
        }

        [HttpPost("add")]
        public async Task<ActionResult<RoleResultDtos>> CreateRoles(CreateRolesDto data)
        {
            try
            {
                if (await this._rolesCommon.RoleExist(data)) return BadRequest("Roles Name already in used");

                
                
                var _role = this._mapper.Map<AppRole>(data);

                _role.Code = _role.Title.GetHashCode()+"$"+_role.Busines.Id;

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
        public ActionResult<IEnumerable<AppRole>> GetAllRoles(int businessid)
        {
            try
            {
                var _role = this._context.Roles.Where(x => x.Status != (int)StatusEnum.delete && x.Busines.Id == businessid).ToList();
                // var result = this._mapper.Map<RolesListResultDto>(_access);
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
                var _access = await this._context.Roles.Where(x => x.Id == data.Id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();

                this._mapper.Map<AppRole>(data);

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<RoleResultDtos>(_access);

                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("The giving role couldn't be edited");
            }
        }
    }
}