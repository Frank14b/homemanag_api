using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.UsersDTOs
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        public string Username {get; set;}

        [Required]
        [MinLength(3)]
        public string Firstname {get; set;}

        public string Lastname {get; set;}

        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password {get; set;}
    }

    public class DeleteUserDto
    {
        [Required]
        public int Id {get; set;}
    }

    public class UpdateStatusUserDto
    {
        [Required]
        public int Id {get; set;}
        
        [Required]
        [EnumDataType(typeof(StatusEnum))]
        public int Status {get; set;}

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdateProfileDto
    {
        [MinLength(1)]
        public string Username {get; set;}

        [MinLength(1)]
        public string Firstname {get; set;}

        [MinLength(1)]
        public string Lastname {get; set;}

        [MinLength(1)]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string CurrentPassword {get; set;}

        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string NewPassword {get; set;}

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class EditUserDto
    {
        public int Id {get; set;}

        [MinLength(1)]
        public string Username {get; set;}

        [MinLength(1)]
        public string Firstname {get; set;}

        [MinLength(1)]
        public string Lastname {get; set;}

        [MinLength(1)]
        [EmailAddress]
        public string Email {get; set;}

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}