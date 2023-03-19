using API.Data;
using API.Entities;
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
        public async Task<Boolean> RoleExist(string title, int businessid, int id)
        {
            if (id == 0)
            {
                var result = await this._context.Roles.Where(x => x.Title.ToLower() == title.ToLower() && x.BusinessId == businessid).AnyAsync();
                return result;
            }
            else
            {
                var result = await this._context.Roles.Where(x => x.Title.ToLower() == title.ToLower() && x.BusinessId == businessid && x.Id != id).AnyAsync();
                return result;
            }
        }

        public AppRole GetRoleById(int id)
        {
            var result = this._context.Roles.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }
    }
}