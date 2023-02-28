
using API.AccessDTOs;
using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class AccessController: BaseApiController
    {
        private readonly DataContext _context;
        public AccessController(DataContext context)
        {
            this._context = context;
        }

        [HttpPost("add")]
        public async Task<ActionResult<AccessResultDto>> CreateAccess(CreateAccessDto data)
        {
            return BadRequest("test");
        }

        [HttpGet("getall")]
        public ActionResult<IEnumerable<AccessResultDto>> GetAllAccess()
        {
            return BadRequest("test");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccessResultDto>> GetOneAccess(int id)
        {
            return BadRequest("test");
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteAccessResultDto>> DeleteAccess(DeleteAccessDto data)
        {
            return BadRequest("test");
        }

        [HttpPut("update")]
        public async Task<ActionResult<AccessResultDto>> UpdateAccess(UpdateAccessDto data)
        {
            return BadRequest("test");
        }
    }
}