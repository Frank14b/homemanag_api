using API.Data;
using API.DTOs.Business;
using API.DTOs.Roles;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Commons
{
    public class BusinessCommon
    {
        private DataContext _context;

        public BusinessCommon(DataContext context)
        {
            this._context = context;
        }
        public async Task<Boolean> BusinessIdExist(int id)
        {
            var result = await this._context.Business.Where(x => x.Status == (int)StatusEnum.enable || x.Id == id).AnyAsync();

            return result;
        }

        public async Task<Boolean> BusinessExist(CreateBusinessDto data, int userid)
        {
            var result = await this._context.Business.Where(x => (x.Name == data.Name && x.User.Id == userid) || x.Reference == data.Reference).AnyAsync();

            return result;
        }
    }
}