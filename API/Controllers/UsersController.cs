using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Commons;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.UsersDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private UsersCommon _userCommon;
        private IMapper _mapper;

        public UsersController(DataContext context, ITokenService tokenService, IMapper mapper)
        {
            this._context = context;
            this._tokenService = tokenService;
            this._mapper = mapper;
            this._userCommon = new UsersCommon(context);
        }

        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            var users = this._context.Users.ToList();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUsers(int id)
        {
            return await this._context.Users.FindAsync(id);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ResultloginDto>> LoginUsers(LoginDto data)
        {
            var result = await this._context.Users.SingleOrDefaultAsync(x => (x.UserName == data.Login) || (x.Email == data.Login));

            if (result == null) Unauthorized("Invalid Login / Password, User not found");

            using var hmac = new HMACSHA512(result.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != result.PasswordHash[i]) return Unauthorized("Invalid Login / Password, User not found");
            }

            var finalresult = this._mapper.Map<ResultloginDto>(result);

            finalresult.Token = this._tokenService.CreateToken(result);

            return finalresult;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> RegisterUsers(RegisterDto data)
        {
            if (await this._userCommon.UserNameExist(data.Username)) return BadRequest("Username already in used");

            if (await this._userCommon.UserEmailExist(data.Email)) return BadRequest("Email Address already in used");

            if (!this._userCommon.IsValidPassword(data.Password)) return BadRequest("Password should have at least 1 lowercase letter, 1 uppercase letter, 1 digit, 1 special character, and at least 8 characters long");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = data.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data.Password)),
                PasswordSalt = hmac.Key,
                FirstName = data.Firstname,
                LastName = data.Lastname,
                Email = data.Email,
                Role = ((int)RoleEnum.user),
                Status = ((int)StatusEnum.enable),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            this._context.Users.Add(user);

            await this._context.SaveChangesAsync();

            return user;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ResultDeleteUserDto>> DeleteUser(DeleteUserDto data)
        {
            var id = (int)data.Id;
            try
            {
                var user = await this._context.Users.Where(x => x.Id == id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();
                user.Status = (int)StatusEnum.delete;
                user.Email = this._userCommon.Deletedkeyword + user.Email;
                user.UserName = this._userCommon.Deletedkeyword + user.UserName;
                await this._context.SaveChangesAsync();

                return new ResultDeleteUserDto
                {
                    Status = true,
                    Message = "User account has been deleted"
                };
            }
            catch (System.Exception)
            {
                return new ResultDeleteUserDto
                {
                    Status = false,
                    Message = "An Error Occured or User account not found"
                };
            }
        }

        [HttpPut("edit")]
        public async Task<ActionResult<ResultUpdateUserDto>> UpdateUser(EditUserDto data)
        {
            try
            {
                var id = (int)data.Id;
                var user = await this._context.Users.Where(x => x.Id == id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();
                if (data.Email.Length > 0) user.Email = data.Email;
                if (data.Firstname.Length > 0) user.FirstName = data.Firstname;
                if (data.Lastname.Length > 0) user.LastName = data.Lastname;
                if (data.Email.Length > 0 && data.Email != user.Email)
                {
                    if (await this._userCommon.UserEmailExist(data.Email)) return BadRequest("Email Address already in used");
                }
                if (data.Username.Length > 0 && data.Username != user.UserName)
                {
                    if (await this._userCommon.UserEmailExist(data.Email)) return BadRequest("Username already in used");
                }

                user.Status = (int)StatusEnum.enable;
                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<ResultUpdateUserDto>(user);

                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut("update-profile")]
        public async Task<ActionResult<ResultUpdateUserDto>> UpdateProfile(UpdateProfileDto data)
        {
            var userIdentity = (User.Identity as ClaimsIdentity);
            var id1 = userIdentity.FindFirst(JwtRegisteredClaimNames.NameId)?.Value;

            // var id1 = this._userCommon.GetConectedUserId();
            var id = 1;
            try
            {
                var user = await this._context.Users.Where(x => x.Id == id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();
                if (data.Firstname != null && data.Firstname.Length > 0) user.FirstName = data.Firstname;
                if (data.Lastname != null && data.Lastname.Length > 0) user.LastName = data.Lastname;
                if (data.Email != null && data.Email.Length > 0 && data.Email != user.Email)
                {
                    if (await this._userCommon.UserEmailExist(data.Email)) return BadRequest("Email Address already in used");
                }
                if (data.Username != null && data.Username.Length > 0 && data.Username != user.UserName)
                {
                    if (await this._userCommon.UserEmailExist(data.Email)) return BadRequest("Username already in used");
                }

                user.Status = (int)StatusEnum.enable;
                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<ResultUpdateUserDto>(user);

                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("current id "+id1);
            }
        }
    }
}