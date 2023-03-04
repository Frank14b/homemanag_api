
using API.Data;
using API.Entities;
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
        public async Task<Boolean> AccessExist(string name, string middleware, int id)
        {
            if(id == 0) return await this._context.Access.Where(x => (x.Name == name || x.MiddleWare == middleware) && x.Status != (int)StatusEnum.delete).AnyAsync();
            else return await this._context.Access.Where(x => (x.Name == name || x.MiddleWare == middleware) && x.Id != id && x.Status != (int)StatusEnum.delete).AnyAsync();
        }
        public async Task<IEnumerable<AppRoleAcces>> UserHasAccess(int roleid)
        {
            var roleaccess = await this._context.Roleaccess.Where(x => x.RoleId == roleid && x.Status == (int)StatusEnum.enable).ToListAsync();
            return roleaccess;
        }
    }
}