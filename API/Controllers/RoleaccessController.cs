
using System.Security.Claims;
using API.Commons;
using API.Data;
using API.DTOs.Roleaccess;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    [Route("/api/roleaccess")]
    public class RoleaccessController : BaseApiController
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private RoleAccessCommon _roleAccessCommon;
        public RoleaccessController(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._roleAccessCommon = new RoleAccessCommon(context);
        }

        [HttpPost("add")]
        public async Task<ActionResult<AppRoleAcces>> AddRoleAccess(RoleaccessPostDto data)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var userid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (await this._roleAccessCommon.RoleAccessExist(data.AccesId, data.RoleId, 0)) return BadRequest("Role access already exist");

                var _data = this._mapper.Map<AppRoleAcces>(data);
                _data.Status = (int)StatusEnum.enable;
                _data.CreatedAt = DateTime.Now;

                var roleaccess = this._context.Roleaccess.Add(_data);

                await this._context.SaveChangesAsync();

                var result = roleaccess;

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest("An error occured. Role Access can't be added " + ex);
            }
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<RoleaccessResultDto>>> GetAll()
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var userid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);
                var roleid = Int32.Parse(currentUser.FindFirst("RoleId").Value);



                var roleaccess = await this._context.Roleaccess.Where(x => x.Status == (int)StatusEnum.enable).Include(p => p.Acces).Include(P => P.Role).ToListAsync();

                var result = this._mapper.Map<IEnumerable<RoleaccessResultDto>>(roleaccess);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest("An error occured. Role Access can't be added " + ex);
            }
        }

        [HttpGet("getbyuser")]
        public async Task<ActionResult<IEnumerable<RoleaccessResultDto>>> GetByUser()
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var userid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);
                var roleid = Int32.Parse(currentUser.FindFirst("RoleId").Value);

                var roleaccess = await this._context.Roleaccess.Where(x => x.Status == (int)StatusEnum.enable).Include(p => p.Acces).Include(P => P.Role).ToListAsync();

                var result = this._mapper.Map<IEnumerable<RoleaccessResultDto>>(roleaccess);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest("An error occured. Role Access can't be added " + ex);
            }
        }
    }
}