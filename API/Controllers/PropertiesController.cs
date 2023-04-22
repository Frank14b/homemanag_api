using System.Net;
using System.Security.Claims;
using API.Commons;
using API.Data;
using API.DTOs.Properties;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize(Policy = "IsUser")]
    [Route("/api/properties")]
    public class PropertiesController : BaseApiController
    {
        private DataContext _context;
        private IMapper _mapper;
        private PropertiesCommon _propertiesCommon;
        public PropertiesController(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._propertiesCommon = new PropertiesCommon(context);
        }

        [HttpPost("add")]
        public async Task<ActionResult<PropertiesResultDto>> CreateProperty(PropertiesCreateDto data)
        {
            try
            {
                // if(await this._propertiesCommon.PropertyExist(data.Reference)) return BadRequest("The Specified Name and SubType already exist");
                var _property = this._mapper.Map<AppProperty>(data);

                this._context.Properties.Add(_property);

                await this._context.SaveChangesAsync();

                return this._mapper.Map<PropertiesResultDto>(_property);
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured. Please retry later");
            }
        }

        [HttpGet("getall")]
        public async Task<ActionResult<PropertiesResultDto>> GetAll(int skip = 0, int limit = 30, string sort = "desc")
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var userId = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

                var query = this._context.Properties.Where(x => x.Status != (int)StatusEnum.delete && x.UserId == userId);

                if (sort == "desc")
                {
                    var _result = await query.OrderByDescending(x => x.CreatedAt).Skip(skip).Take(limit).ToListAsync();
                    var result = this._mapper.Map<IEnumerable<PropertiesResultListDto>>(_result);
                    var rs = new PropertiesResultDto
                    {
                        Data = result,
                        Limit = limit,
                        Skip = skip,
                        Total = query.Count()
                    };

                    return Ok(rs);
                }
                else
                {
                    var _result = await query.OrderBy(x => x.CreatedAt).Skip(skip).Take(limit).ToListAsync();
                    var result = this._mapper.Map<IEnumerable<PropertiesResultListDto>>(_result);
                    var rs = new PropertiesResultDto
                    {
                        Data = result,
                        Limit = limit,
                        Skip = skip,
                        Total = query.Count()
                    };

                    return Ok(rs);
                }
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured. Please retry later");
            }
        }
    }
}