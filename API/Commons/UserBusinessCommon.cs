using System.Collections;
using System.Text.RegularExpressions;
using API.Data;
using API.Entities;
using API.UsersDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Commons
{
    public class UserBusinessCommon
    {
        private readonly DataContext _context;
        public UserBusinessCommon(DataContext context)
        {
            this._context = context;
        }

        public async Task<Boolean> UserBusinessExist(int id)
        {
            var result = await this._context.UserBusiness.Where(x => x.Status == (int)StatusEnum.enable || x.Id == id).AnyAsync();
            return result;
        }
    }
}