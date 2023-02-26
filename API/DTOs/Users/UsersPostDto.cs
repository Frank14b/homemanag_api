using System.ComponentModel.DataAnnotations;

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

        public int Status {get; set;}

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

    public class UpdateProfileDto
    {
        public string Username {get; set;}

        public string Firstname {get; set;}

        public string Lastname {get; set;}

        public string Email {get; set;}

        // [Required]
        // [MinLength(8)]
        // [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string CurrentPassword {get; set;}

        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string NewPassword {get; set;}
    }

    public class EditUserDto
    {
        public int Id {get; set;}
        public string Username {get; set;}

        public string Firstname {get; set;}

        public string Lastname {get; set;}

        public string Email {get; set;}
    }
}