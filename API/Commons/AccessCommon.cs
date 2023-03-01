
using API.AccessDTOs;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Commons
{
    public class AccessCommon : ControllerBase
    {
         private DataContext _context;

        public AccessCommon(DataContext context)
        {
            this._context = context;
        }
        public async Task<Boolean> AccessExist(CreateAccessDto data)
        {
            return await this._context.Access.Where(x => x.Name == data.Name || x.MiddleWare == data.MiddleWare).AnyAsync();
        }
    }
}