using System.Net;
using System.Security.Claims;
using API.Commons;
using API.Data;
using API.DTOs.Emails;
using API.DTOs.Properties;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize(Policy = "IsUser")]
    [Route("/api/properties")]
    public class PropertiesController : BaseApiController
    {
        private DataContext _context;
        private IMapper _mapper;
        private PropertiesCommon _propertiesCommon;
        private EmailsCommon _emailsCommons;
        public PropertiesController(DataContext context, IMapper mapper, IMailService mailService)
        {
            this._context = context;
            this._mapper = mapper;
            this._propertiesCommon = new PropertiesCommon(context);
            this._emailsCommons = new EmailsCommon(mailService);
        }

        [HttpPost("add")]
        public async Task<ActionResult<PropertiesResultListDto>> CreateProperty(PropertiesCreateDto data)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var userId = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);
                
                AppProperty _property = this._mapper.Map<AppProperty>(data);

                _property.UserId = userId;
                _property.Status = (int)StatusEnum.enable;
                _property.CreatedAt = DateTime.Now;
                _property.Reference = userId +"-"+ _property.Name.GetHashCode();
                _property.slug = _property.Name.Replace(" ", "-") +"-"+ _property.Reference.GetHashCode();

                // if(await this._propertiesCommon.PropertyExist(_property.Reference)) return BadRequest("The Specified Name already exist");

                this._context.Properties.Add(_property);

                await this._context.SaveChangesAsync();

                PropertiesResultListDto result = this._mapper.Map<PropertiesResultListDto>(_property);

                AppUser userData = await this._context.Users.Where((x) => x.Id == userId).FirstOrDefaultAsync();

                var data_email = new EmailRequestDto
                {
                    ToEmail = userData.Email,
                    ToName = userData.FirstName,
                    SubTitle = "Confirmation of Property Creation",
                    ReplyToEmail = "",
                    Subject = "Confirmation of Property Creation",
                    Body = this._emailsCommons.PropertyCreate(result),
                    Attachments = { }
                };
                await this._emailsCommons.SendMail(data_email);

                return result;
            }
            catch (System.Exception th)
            {
                return BadRequest("An error occured. Please retry later "+ th);
            }
        }

        [HttpGet("getall")]
        public async Task<ActionResult<PropertiesResultDto>> GetAll(int skip = 0, int limit = 30, string sort = "desc")
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var userId = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

                var query = this._context.Properties.Where(x => x.Status != (int)StatusEnum.delete && x.UserId == userId);

                if (sort == "desc")
                {
                    var _result = await query.OrderByDescending(x => x.CreatedAt).Skip(skip).Take(limit).ToListAsync();
                    var result = this._mapper.Map<IEnumerable<PropertiesResultListDto>>(_result);
                    var rs = new PropertiesResultDto
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
                    var result = this._mapper.Map<IEnumerable<PropertiesResultListDto>>(_result);
                    var rs = new PropertiesResultDto
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
                return BadRequest("An error occured. Please retry later");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<PropertiesDeleteDto>> DeleteBusiness(int id)
        {
            try
            {
                var _property = await this._context.Properties.Where(x => x.Id == id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();
                _property.Status = (int)StatusEnum.delete;
                await this._context.SaveChangesAsync();

                return new PropertiesDeleteDto
                {
                    Status = true,
                    Message = "The giving Property has been deleted"
                };
            }
            catch (System.Exception)
            {
                return new PropertiesDeleteDto
                {
                    Status = false,
                    Message = "The giving Property counldn't been deleted"
                };
            }
        }

        [HttpPut("status/{id}")]
        public async Task<ActionResult<PropertiesDeleteDto>> UpdateStatus(int id)
        {
            try
            {
                var _property = await this._context.Properties.Where(x => x.Id == id && x.Status != (int)StatusEnum.delete).FirstOrDefaultAsync();
                
                if(_property.Status == (int)StatusEnum.enable) { _property.Status = (int)StatusEnum.disable; } else { _property.Status = (int)StatusEnum.enable; };
                await this._context.SaveChangesAsync();

                return new PropertiesDeleteDto
                {
                    Status = true,
                    Message = "The giving Property has been updated"
                };
            }
            catch (System.Exception)
            {
                return new PropertiesDeleteDto
                {
                    Status = false,
                    Message = "The giving Property counldn't been updated"
                };
            }
        }
        
    }
}