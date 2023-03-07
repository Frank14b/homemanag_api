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
    [Authorize]
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

        [HttpPost("add")]
        public async Task<ActionResult<PropertyTResultDto>> CreateType(PropertyTCreateDto data)
        {
            try
            {
                if(await this._propertiesCommon.TypeExist(data.Name, data.SubTypeId)) return BadRequest("The Specified Name and SubType already exist");

                var _type = this._mapper.Map<AppPropertyType>(data);
                _type.CreatedAt = DateTime.Now;
                _type.Status = (int)StatusEnum.enable;

                var _data = this._context.PropertyTypes.Add(_type);

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<PropertyTResultDto>(_data);

                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured. Type can't be created");
            }
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<PropertyTResultDto>>> GetAllType()
        {
            try
            {
                var _types = await this._context.PropertyTypes.Where(x => x.Status != (int)StatusEnum.delete).ToListAsync();
                var result = this._mapper.Map<IEnumerable<PropertyTResultDto>>(_types);

                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured. Please retry later");
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