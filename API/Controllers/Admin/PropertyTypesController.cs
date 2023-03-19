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
                if(await this._propertiesCommon.TypeExist(data.Name, data.SubTypeId)) return BadRequest("The Specified Name and SubType already exist");

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
        public async Task<ActionResult<PropertyTResultDto>> EditType(PropertyTUpdateDto data)
        {
            try
            {
                if(await this._propertiesCommon.TypeExist(data.Name, data.SubTypeId)) return BadRequest("The Specified Name and SubType already exist");

                var _type = this._mapper.Map<AppPropertyType>(data);
                _type.CreatedAt = DateTime.Now;
                _type.Status = (int)StatusEnum.enable;

                this._context.PropertyTypes.Add(_type);

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<PropertyTResultDto>(_type);

                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured. Type can't be created");
            }
        }

        [AllowAnonymous]
        [HttpDelete("emptyTable")]
        public async Task<ActionResult<Boolean>> EmptyTable()
        {
            try
            {
                await this._context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE PropertyTypes");
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}