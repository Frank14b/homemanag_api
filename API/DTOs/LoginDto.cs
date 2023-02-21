using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Login {get; set;}

        public string Password {get; set;}
    }
}