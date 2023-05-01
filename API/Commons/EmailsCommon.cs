using API.DTOs.Business;
using API.DTOs.Emails;
using API.DTOs.Properties;
using API.Entities;
using API.Interfaces;
using API.UsersDTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Commons
{
    public class EmailsCommon : ControllerBase
    {
        private readonly IMailService mailService;
        public EmailsCommon(IMailService mailService)
        {
            this.mailService = mailService;
        }

        public async Task<Boolean> SendMail([FromForm] EmailRequestDto request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string UserLoginBody(ResultloginDto data)
        {
            string body = "<div> <p>Dear " + data.FirstName + ",</p><br/>"
                           + "<p>We are pleased to inform you that you have successfully logged in to your account on " + DateTime.UtcNow
                           + " GMT from .This email is to confirm that the login was authorized by you to access your account.</p>"
                           + "<br/> <p>If you did not authorize this login, please contact us immediately at support.auth@homemanag.net"
                           + " We take the security of your account very seriously and will investigate any suspicious activity.</p>"
                           + "<br/><p>Thank you for choosing our services.</p>"
                           + "</div>";

            return body;
        }

        public string UserRegisterBody(AppUser data)
        {
            string body = "<div> <p>Dear " + data.FirstName + ",</p><br/>"
                           + "<p>Thank you for signing up for our services! We are thrilled to have you as a part of our community. Your account has been successfully created the " + DateTime.UtcNow
                           + " GMT from .This email is to confirm that the registration was authorized by you.</p>"
                           + "<br/> <p>If you did not perform this action, please contact us immediately at support.auth@homemanag.net"
                           + " We take the security very seriously and will investigate any suspicious activity.</p>"
                           + "<br/><p>Thank you for choosing our services.</p>"
                           + "</div>";

            return body;
        }

        public string BusinessCreate(BusinessResultListDto data)
        {
            string body = "<div><p>We are pleased to inform you that the requested Business <b>"+data.Name+"</b> has been successfully created and is now ready for use.</p></div>";

            return body;
        }

        public string PropertyCreate(PropertiesResultListDto data)
        {
            string body = "<div><p>We are pleased to inform you that the requested Property <b>"+data.Name+"</b> has been successfully created and is now ready for use.</p></div>";

            return body;
        }
    }
}