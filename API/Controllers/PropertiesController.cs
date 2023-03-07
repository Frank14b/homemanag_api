using System.Net;
using API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("/api/properties")]
    public class PropertiesController : BaseApiController
    {
        private DataContext _context;
        private IMapper _mapper;
        public PropertiesController(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        // [HttpPost("add")]
        // public async Task<ActionResult<>> CreateProperty()
        // {
        //     try
        //     {
                
        //     }
        //     catch (System.Exception)
        //     {
        //         throw;
        //     }
        // }
    }
}