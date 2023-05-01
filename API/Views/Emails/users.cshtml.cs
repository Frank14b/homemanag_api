using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace API.Views.Emails
{
    public class users : PageModel
    {
        private readonly ILogger<users> _logger;

        public users(ILogger<users> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}