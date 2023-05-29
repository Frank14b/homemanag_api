using System.ComponentModel.DataAnnotations;

namespace API.DTOs.UserBusiness
{
    public class UserBusinessDto
    {
        [Required]
        public int UserId {get; set;}

        [Required]
        public int BusinesId {get; set;}

        [Required]
        public Boolean IsAdmin {get; set;}
    }
}