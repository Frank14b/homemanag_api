
using API.Data;
using API.DTOs.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class RolesController : BaseApiController
    {
        private readonly DataContext _context;
        public RolesController(DataContext context) 
        {
            this._context = context;
        }

        [HttpPost("add")]
        public async Task<ActionResult<RoleResultDtos>> CreateRole(CreateRolesDto data)
        {
            return BadRequest("test");
        }

        [HttpGet("getall")]
        public ActionResult<IEnumerable<RoleResultDtos>> GetAllRoles()
        {
            return BadRequest("test");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleResultDtos>> GetOneRole(int id)
        {
            return BadRequest("test");
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteRoleResultDto>> DeleteRole(DeleteRolesDto data)
        {
            return BadRequest("test");
        }

        [HttpPut("update")]
        public async Task<ActionResult<RoleResultDtos>> UpdateRole(UpdateRolesDto data)
        {
            return BadRequest("test");
        }
    }
}