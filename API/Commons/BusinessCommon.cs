using System.Collections;
using API.Data;
using API.DTOs.Business;
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
            var result = await this._context.Business.Where(x => x.Status == (int)StatusEnum.enable && x.Id == id).AnyAsync();
            return result;
        }

        public async Task<Boolean> BusinessExist(AppBusiness data, int userid)
        {
            var result = await this._context.Business.Where(x => (x.Name.ToLower() == data.Name.ToLower() && x.UserId == userid) || x.Reference.ToLower() == data.Reference.ToLower()).AnyAsync();
            return result;
        }

        public AppBusiness GetBusinessById(int id)
        {
            var result = this._context.Business.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public TotalBusinessDto GetTotalBusiness(int userid = 0)
        {
            var resultActive = this._context.Business.Where(x => x.Status == (int)StatusEnum.enable).ToList();

            var resultInactive = this._context.Business.Where(x => x.Status == (int)StatusEnum.disable).ToList();
            
            var result = new TotalBusinessDto{
                active = resultActive.Count,
                inactive = resultInactive.Count
            };

            return result;
        }

        public ArrayList GetUserBusiness(int userid)
        {
            var result = this._context.Business.Where(x => x.UserId == userid && x.Status == (int)StatusEnum.enable).ToList();

            ArrayList _data = new ArrayList();
            for (int i = result.Count - 1; i >= 0 ; i--)
            {
                _data.Add(result[i].Id); 
            }

            return _data;
        }
    }
}