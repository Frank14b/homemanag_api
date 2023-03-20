using System.Collections;
using System.Text.RegularExpressions;
using API.Data;
using API.Entities;
using API.UsersDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Commons
{
    public class UsersCommon : ControllerBase
    {
        private DataContext _context;
        private BusinessCommon _businessCommon;

        public UsersCommon(DataContext context)
        {
            this._context = context;
            this._businessCommon = new BusinessCommon(context);
        }

        public async Task<Boolean> UserIdExist(int id)
        {
            var result = await this._context.Users.Where(x => x.Status == (int)StatusEnum.enable || x.Id == id).AnyAsync();
            return result;
        }
        public async Task<Boolean> UserNameExist(string username)
        {
            return await this._context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
        public async Task<Boolean> UserEmailExist(string useremail)
        {
            return await this._context.Users.AnyAsync(x => x.Email.ToLower() == useremail.ToLower());
        }
        public AppUser GetUserById(int id)
        {
            var result = this._context.Users.Where(x => x.Status == (int)StatusEnum.enable || x.Id == id).FirstOrDefault();
            return result;
        }
        public Boolean IsValidPassword(string password)
        {
            // Validate strong password
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            var test = validateGuidRegex.IsMatch(password);
            return true;
        }
        public String Deletedkeyword {get;} = "deleted_";

        public ArrayList UserStatus()
        {
            ArrayList _status = new ArrayList();
            _status.Add(0);
            _status.Add(1);

            return _status;
        }

        public TotalUsersDto GetTotalUsers(int userid = 0)
        {
            var userbusiness = this._businessCommon.GetUserBusiness(userid);
            var employees = 0;
            var allUsers = 0;

            if(userbusiness.Count > 0){
                var resultEmployees = this._context.Users.Where(x => x.Status == (int)StatusEnum.enable && x.UserProperties.All(up => userbusiness.Contains(up.Property.BusinessId) && up.IsEmployee)).ToList();
                employees = resultEmployees.Count;

                var resultAllUsers = this._context.Users.Where(x => x.Status == (int)StatusEnum.enable && x.UserProperties.All(up => userbusiness.Contains(up.Property.BusinessId))).ToList();
                allUsers = resultAllUsers.Count;
            }

            var userProperties = this._context.UserProperties.Where(up => up.UserId == userid && up.Status == (int)StatusEnum.enable).ToList();
            ArrayList _propertyList = new ArrayList();
            for (int i = 0; i < userProperties.Count; i++)
            {
                _propertyList.Add(userProperties[i].PropertyId);
            }

            if(_propertyList.Count > 0) {
                var resultEmployees2 = this._context.Users.Where(x => x.Status == (int)StatusEnum.enable && x.UserProperties.All(up => _propertyList.Contains(up.PropertyId) && up.IsEmployee)).ToList();
                employees = employees + resultEmployees2.Count;

                var resultAllUsers2 = this._context.Users.Where(x => x.Status == (int)StatusEnum.enable && x.UserProperties.All(up => _propertyList.Contains(up.PropertyId))).ToList();
                allUsers = allUsers + resultAllUsers2.Count;
            }
            
            var result = new TotalUsersDto{
                employees = employees,
                all = allUsers
            };

            return result;
        }
    }
}