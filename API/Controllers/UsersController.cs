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
    [Authorize(Policy = "IsUser")]
    [Route("/api/users")]
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

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<ResultAllUserDto>>> GetUsers()
        {
            var users = await this._context.Users.Where(x => x.Role != (int)RoleEnum.suadmin && x.Status != (int)StatusEnum.delete).ToListAsync();

            var result = this._mapper.Map<IEnumerable<ResultAllUserDto>>(users);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultAllUserDto>> GetUsers(int id)
        {
            var user = await this._context.Users.Where(x => x.Id == id).FirstAsync();

            var result = this._mapper.Map<ResultAllUserDto>(user);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ResultloginDto>> LoginUsers(LoginDto data)
        {
            try
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
            catch (System.Exception)
            {

                return BadRequest("An error occured or user not found");
            }
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

        [HttpPost("validate-token")]
        public async Task<ActionResult<ResultAllUserDto>> ValidateToken()
        {
            ClaimsPrincipal currentUser = this.User;
            var id = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (!await this._userCommon.UserIdExist(id)) return BadRequest("User not found");

            var user = await this._context.Users.Where(x => x.Id == id).FirstAsync();

            var result = this._mapper.Map<ResultAllUserDto>(user);

            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ResultDeleteUserDto>> DeleteUser(DeleteUserDto data)
        {
            try
            {
                var id = (int)data.Id;

                if (await this._userCommon.UserIdExist(id)) return BadRequest("User not found");

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

                if (await this._userCommon.UserIdExist(id)) return BadRequest("User not found");

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
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (await this._userCommon.UserIdExist(id)) return BadRequest("User not found");

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
                return BadRequest("An error occured or User not found");
            }
        }

        [HttpPut("status")]
        public async Task<ActionResult<ResultUpdateUserDto>> ChangeStatus(UpdateStatusUserDto data)
        {
            try
            {
                var id = data.Id;
                var _status = this._userCommon.UserStatus();

                if (await this._userCommon.UserIdExist(id)) return BadRequest("User not found");

                var user = await this._context.Users.Where(x => x.Id == id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();
                if (!_status.Contains(data.Status)) return BadRequest("User status should be 0:inactive or 1:active");
                user.Status = data.Status;

                await this._context.SaveChangesAsync();

                return this._mapper.Map<ResultUpdateUserDto>(user);
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured or user not found");
            }
        }
    }
}