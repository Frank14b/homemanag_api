using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Commons
{
    public class PropertiesCommon : ControllerBase
    {
        private DataContext _context;
        public PropertiesCommon(DataContext context)
        {
            this._context = context;
        }

        public async Task<Boolean> TypeExist(string name, int subTypeId)
        {
            var _type = await this._context.PropertyTypes.Where(x => (x.Name == name && x.SubTypeId == subTypeId && x.Status != (int)StatusEnum.delete)).AnyAsync();
            return _type;
        }
    }
}