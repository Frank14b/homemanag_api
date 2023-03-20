using API.Data;
using API.DTOs.Properties;
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
            var _type = await this._context.PropertyTypes.Where(x => (x.Name.ToLower() == name.ToLower() && x.SubTypeId == subTypeId && x.Status != (int)StatusEnum.delete)).AnyAsync();
            return _type;
        }

        public async Task<Boolean> PropertyExist(string reference)
        {
            var _type = await this._context.Properties.Where(x => (x.Reference == reference && x.Status != (int)StatusEnum.delete)).AnyAsync();
            return _type;
        }

        public TotalPropertiesDto GetTotalProperties(int userid = 0)
        {
            var resultActive = this._context.Properties.Where(x => x.Status == (int)StatusEnum.enable).ToList();

            var resultInactive = this._context.Properties.Where(x => x.Status == (int)StatusEnum.disable).ToList();
            
            var result = new TotalPropertiesDto{
                active = resultActive.Count,
                inactive = resultInactive.Count
            };

            return result;
        }  
    }
}