using API.Commons;
using API.Data;
using API.DTOs.Properties;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Admin
{
    [Authorize(Policy = "IsAdmin")]
    [Route("/api/admin/propertytypes")]
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

        [HttpPost("add")]
        public async Task<ActionResult<PropertyTResultDto>> CreateType(PropertyTCreateDto data)
        {
            try
            {
                if (await this._propertiesCommon.TypeExist(data.Name, data.SubTypeId)) return BadRequest("The Specified Name and SubType already exist");

                var _type = this._mapper.Map<AppPropertyType>(data);
                _type.CreatedAt = DateTime.Now;
                _type.Status = (int)StatusEnum.enable;

                this._context.PropertyTypes.Add(_type);

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<PropertyTResultDto>(_type);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest("An error occured. Type can't be created" + ex);
            }
        }

        [HttpPut("edit")]
        public async Task<ActionResult<PropertyTResultListDto>> EditType(PropertyTUpdateDto _data)
        {
            try
            {
                if (await this._propertiesCommon.TypeExist(_data.Name, _data.SubTypeId)) return BadRequest("The Specified Name and SubType already exist");

                var data = this._context.PropertyTypes.Where(x => x.Id == _data.Id).FirstOrDefault();

                if(data == null) {
                    return BadRequest("Type not found");
                }

                data.Name = _data.Name;
                data.Description = _data.Description;
                data.SubTypeId = _data.SubTypeId;
                data.UpdatedAt = DateTime.Now;
                data.Status = (int)StatusEnum.enable;

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<PropertyTResultListDto>(data);

                return result;
            }
            catch (System.Exception th)
            {
                return BadRequest("An error occured. Type can't be updated " + th);
            }
        }

        [HttpGet("getall")]
        public async Task<ActionResult<PropertyTResultDto>> GetAllType(int skip = 0, int limit = 50000, string order = "desc")
        {
            try
            {
                var query = this._context.PropertyTypes.Where(x => x.Status != (int)StatusEnum.delete);

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

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<PropertyTDeleteDto>> DeleteType(int id)
        {
            try
            {
                var data = this._context.PropertyTypes.Where(x => x.Id == id).FirstOrDefault();

                if(data == null) {
                    return BadRequest("Type not found");
                }

                data.Status = (int) StatusEnum.delete;
                data.UpdatedAt = DateTime.Now;

                await this._context.SaveChangesAsync();

                return new PropertyTDeleteDto {
                    Status = true,
                    Message = "The type has been deleted"
                };
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured. Please retry later");
            }
        }
    }
}