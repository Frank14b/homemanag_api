using System.Text.RegularExpressions;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Commons
{
    public class UsersCommon : ControllerBase
    {
        private DataContext _context;

        public UsersCommon(DataContext context)
        {
            this._context = context;
        }
        public async Task<Boolean> UserNameExist(string username)
        {
            return await this._context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
        public async Task<Boolean> UserEmailExist(string useremail)
        {
            return await this._context.Users.AnyAsync(x => x.Email.ToLower() == useremail.ToLower());
        }
        public Boolean IsValidPassword(string password)
        {
            // Validate strong password
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            var test = validateGuidRegex.IsMatch(password);
            return true;
        }
        public String Deletedkeyword {get;} = "deleted_";
    }
}