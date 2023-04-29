using API.DTOs.Emails;
using API.Interfaces;
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
    }
}