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
    [Route("/api/propertytypes")]
    public class PropertyTypesController : BaseApiController
    {
        private DataContext _context;
        private IMapper _mapper;
        private PropertiesCommon _propertiesCommon;
        public PropertyTypesController(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._propertiesCommon = new PropertiesCommon(context);
        }

        [HttpGet("getall")]
        public async Task<ActionResult<PropertyTResultDto>> GetAllType(int skip = 0, int limit = 50000, string order = "desc")
        {
            try
            {
                var query = this._context.PropertyTypes.Where(x => x.Status == (int)StatusEnum.enable);

                if (order == "desc")
                {
                    var _types = await query.OrderByDescending(x => x.CreatedAt).Skip(skip).Take(limit).ToListAsync();
                    var result = this._mapper.Map<IEnumerable<PropertyTResultListDto>>(_types);

                    var rs = new PropertyTResultDto
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
                    var _types = await query.OrderBy(x => x.CreatedAt).Skip(skip).Take(limit).ToListAsync();
                    var result = this._mapper.Map<IEnumerable<PropertyTResultListDto>>(_types);

                    var rs = new PropertyTResultDto
                    {
                        Data = result,
                        Limit = limit,
                        Skip = skip,
                        Total = query.Count()
                    };

                    return Ok(rs);
                }
            }
            catch (System.Exception th)
            {
                return BadRequest("An error occured. Please retry later" + th);
            }
        }

        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<PropertyTResultDto>> GetTypeById(int id)
        {
            try
            {
                var _types = await this._context.PropertyTypes.Where(x => x.Id == id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();
                var result = this._mapper.Map<PropertyTResultDto>(_types);

                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured. Please retry later");
            }
        }
    }
}