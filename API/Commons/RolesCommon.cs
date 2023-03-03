using API.Data;
using API.DTOs.Roles;
using Microsoft.EntityFrameworkCore;

namespace API.Commons
{
    public class RolesCommon
    {
        private DataContext _context;

        public RolesCommon(DataContext context)
        {
            this._context = context;
        }
        public async Task<Boolean> RoleExist(CreateRolesDto data)
        {
            var result = await this._context.Roles.Where(x => x.Title == data.Title || x.Busines.Id == data.BusinesId).AnyAsync();

            return result;
        }
    }
}