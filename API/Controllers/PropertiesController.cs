using System.Net;
using API.Commons;
using API.Data;
using API.DTOs.Properties;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}