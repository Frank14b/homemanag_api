using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Commons
{
    public class RoleAccessCommon
    {
        private DataContext _context;
        public RoleAccessCommon(DataContext context)
        {
            this._context = context;
        }
        public async Task<Boolean> RoleAccessExist(int accessid, int roleid, int id)
        {
            if (id == 0)
            {
                var result = await this._context.Roleaccess.Where(x => x.AccesId == accessid && x.RoleId == roleid && x.Status == (int)StatusEnum.enable).AnyAsync();
                return result;
            }
            else
            {
                var result = await this._context.Roleaccess.Where(x => x.AccesId == accessid && x.RoleId == roleid && x.Status == (int)StatusEnum.enable && x.Id != id).AnyAsync();
                return result;
            }
        }

        public AppRoleAcces GetRoleAccessById(int id)
        {
            var result = this._context.Roleaccess.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }
    }
}