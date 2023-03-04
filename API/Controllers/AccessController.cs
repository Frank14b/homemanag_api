
using API.AccessDTOs;
using API.Commons;
using API.Data;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class AccessController : BaseApiController
    {
        private readonly DataContext _context;

        private AccessCommon _accessCommon;

        private IMapper _mapper;
        public AccessController(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._accessCommon = new AccessCommon(context);
        }

        [HttpPost("add")]
        public async Task<ActionResult<AccessResultDto>> CreateAccess(CreateAccessDto data)
        {
            try
            {
                if (await this._accessCommon.AccessExist(data.Name, data.MiddleWare, 0)) return BadRequest("Access Name | MiddleWare already in used");
                
                var _access = this._mapper.Map<AppAcces>(data);

                this._context.Access.Add(_access);

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<AccessResultDto>(_access);

                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("Access can't be created");
            }
        }
        
        [HttpGet("getall")]
        public ActionResult<IEnumerable<AccessListResultDto>> GetAllAccess()
        {
            try
            {
                var _access = this._context.Access.Where(x => x.Status != (int)StatusEnum.delete).ToList();
                var result = this._mapper.Map<IEnumerable<AccessListResultDto>>(_access);

                return Ok(result);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccessResultDto>> GetOneAccess(int id)
        {
            try
            {
                var _access = await this._context.Access.Where(x => x.Id == id && x.Status == (int)StatusEnum.enable).FirstOrDefaultAsync();
                if(_access == null) return NotFound("Access Not Found or Invalid Access ID");
                var result = this._mapper.Map<AccessResultDto>(_access);
                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured or access not found");
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteAccessResultDto>> DeleteAccess(DeleteAccessDto data)
        {
            try
            {
                var _access = await this._context.Access.Where(x => x.Id == data.Id && x.Status == (int)StatusEnum.enable).FirstOrDefaultAsync();
                if(_access == null) return NotFound("Access Not Found or Invalid ID");
                
                _access.Status = (int)StatusEnum.delete;
                await this._context.SaveChangesAsync();

                return new DeleteAccessResultDto
                {
                    Status = true,
                    Message = "The giving Access has been deleted"
                };
            }
            catch (System.Exception)
            {
                return new DeleteAccessResultDto
                {
                    Status = false,
                    Message = "The giving Access counldn't been deleted"
                };
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<AccessResultDto>> UpdateAccess(UpdateAccessDto data)
        {
            try
            {
                var _access = await this._context.Access.Where(x => x.Id == data.Id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();
                
                if(_access == null) return NotFound("Access Not Found or Invalid ID");

                _access.Name = (data.Name != null) ? data.Name : _access.Name;
                _access.Description = (data.Description != null) ? data.Description : _access.Description;
                _access.MiddleWare = (data.MiddleWare != null) ? data.MiddleWare : _access.MiddleWare;
                _access.ApiPath = (data.ApiPath != null) ? data.ApiPath : _access.ApiPath;

                if (await this._accessCommon.AccessExist(_access.Name, _access.MiddleWare, _access.Id)) return BadRequest("Access Name | MiddleWare already in used");

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<AccessResultDto>(_access);

                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("The giving access couldn't been edited");
            }
        }
    }
}