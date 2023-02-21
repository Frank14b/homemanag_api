
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username {get; set;}

        [Required]
        public string Firstname {get; set;}

        public string Lastname {get; set;}

        public int Status {get; set;}

        [Required]
        public string Email {get; set;}

        [Required]
        public string Password {get; set;}
    }
}