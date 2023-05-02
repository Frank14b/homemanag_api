using System.Security.Claims;
using API.Commons;
using API.Data;
using API.DTOs.Business;
using API.DTOs.Emails;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize(Policy = "IsUser")]
    [Route("/api/business")]
    public class BusinessController: BaseApiController
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private BusinessCommon _businessCommon;
        private UsersCommon _userCommon;

        private EmailsCommon _emailsCommons;

        public BusinessController(DataContext context, IMapper mapper, IMailService mailService)
        {
            this._context = context;
            this._mapper = mapper;
            this._businessCommon = new BusinessCommon(context);
            this._userCommon = new UsersCommon(context);
            this._emailsCommons = new EmailsCommon(mailService);
        }

        [HttpPost("add")]
        public async Task<ActionResult<BusinessResultListDto>> CreateBusiness(CreateBusinessDto data)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var userid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

                var _business = this._mapper.Map<AppBusiness>(data);
                _business.Reference = Math.Abs(_business.Name.GetHashCode())+"_"+userid;
                _business.UserId = userid;
                _business.Status = (int)StatusEnum.enable;
                _business.CreatedAt = DateTime.UtcNow;

                if (await this._businessCommon.BusinessExist(_business, userid)) return  BadRequest("Business Name already in used");

                this._context.Business.Add(_business);

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<BusinessResultListDto>(_business);

                AppUser userData = await this._context.Users.Where((x) => x.Id == userid).FirstOrDefaultAsync();

                var data_email = new EmailRequestDto
                {
                    ToEmail = userData.Email,
                    ToName = userData.FirstName,
                    SubTitle = "Confirmation of Business Creation",
                    ReplyToEmail = "",
                    Subject = "Confirmation of Business Creation",
                    Body = this._emailsCommons.BusinessCreate(result),
                    Attachments = { }
                };
                await this._emailsCommons.SendMail(data_email);

                return result;
            }
            catch (System.Exception ex)
            {
                return BadRequest("Business can't be created " + ex);
            }
        }
        
        [HttpGet("getall")]
        public async Task<ActionResult<BusinessResultDto>> GetAllBusiness(int skip = 0, int limit = 30, string sort = "desc")
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var userId = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

                var query = this._context.Business.Where(x => x.Status != (int)StatusEnum.delete && x.UserId == userId);

                if (sort == "desc")
                {
                    var _result = await query.OrderByDescending(x => x.CreatedAt).Skip(skip).Take(limit).ToListAsync();
                    var result = this._mapper.Map<IEnumerable<BusinessResultListDto>>(_result);
                    var rs = new BusinessResultDto
                    {
                        Data = result,
                        Limit = limit,
                        Skip = skip,
                        Total = query.Count()
                    };

                    return Ok(rs);
                }
                else
                {
                    var _result = await query.OrderBy(x => x.CreatedAt).Skip(skip).Take(limit).ToListAsync();
                    var result = this._mapper.Map<IEnumerable<BusinessResultListDto>>(_result);
                    var rs = new BusinessResultDto
                    {
                        Data = result,
                        Limit = limit,
                        Skip = skip,
                        Total = query.Count()
                    };

                    return Ok(rs);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("{businessid}")]
        public async Task<ActionResult<BusinessResultListDto>> GetOneBusiness(int businessid)
        {
            try
            {
                var _business = await this._context.Business.Where(x => x.Id == businessid && x.Status == (int)StatusEnum.enable).Include(p => p.User).FirstOrDefaultAsync();
                if(_business == null) return NotFound("Business not Found or invalid ID");

                var result = this._mapper.Map<BusinessResultListDto>(_business);

                return result;
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured or business not found");
            }
        }

        [HttpGet("getbyuser/{userid}")]
        public ActionResult<IEnumerable<BusinessResultListDto>> GetByUser(int userid)
        {
            try
            {
                var _business = this._context.Business.Where(x => x.UserId == userid && x.User.Status == (int)StatusEnum.enable && x.Status == (int)StatusEnum.enable).ToList();
                if(_business == null) return NotFound("Business not Found or invalid ID");

                var result = this._mapper.Map<IEnumerable<BusinessResultListDto>>(_business);
                
                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest("An error occured or business not found");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<DeleteBusinessResultDto>> DeleteBusiness(int id)
        {
            try
            {
                var _business = await this._context.Business.Where(x => x.Id == id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();
                _business.Status = (int)StatusEnum.delete;
                await this._context.SaveChangesAsync();

                return new DeleteBusinessResultDto
                {
                    Status = true,
                    Message = "The giving Business has been deleted"
                };
            }
            catch (System.Exception)
            {
                return new DeleteBusinessResultDto
                {
                    Status = false,
                    Message = "The giving Business counldn't been deleted"
                };
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<BusinessResultListDto>> UpdateBusiness(int id, UpdateBusinessDto data)
        {
            try
            {
                var _business = await this._context.Business.Where(x => x.Id == id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();

                if(_business == null) return NotFound("Business not Found");

                _business.UpdatedAt = DateTime.UtcNow;
                _business.Name = data.Name;
                _business.Description = data.Description;
                _business.Country = data.Country;
                _business.Email = data.Email;
                _business.CountryCode = data.CountryCode;
                _business.PhoneNumber = data.PhoneNumber;
                _business.Address = data.Address;

                await this._context.SaveChangesAsync();

                var result = this._mapper.Map<BusinessResultListDto>(_business);

                return result;
            }
            catch (System.Exception th)
            {
                return BadRequest("The giving business couldn't be edited" + th);
            }
        }

        [HttpPut("status/{id}")]
        public async Task<ActionResult<DeleteBusinessResultDto>> UpdateStatus(int id)
        {
            try
            {
                var _business = await this._context.Business.Where(x => x.Id == id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();
                
                if(_business.Status == (int)StatusEnum.enable) { _business.Status = (int)StatusEnum.disable; } else { _business.Status = (int)StatusEnum.enable; };
                await this._context.SaveChangesAsync();

                return new DeleteBusinessResultDto
                {
                    Status = true,
                    Message = "The giving Business has been updated"
                };
            }
            catch (System.Exception)
            {
                return new DeleteBusinessResultDto
                {
                    Status = false,
                    Message = "The giving Business counldn't been updated"
                };
            }
        }
    }
}