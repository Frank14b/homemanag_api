
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Roles
{
    public class CreateRolesDto
    {
        [Required]
        public string Title {get; set;}

        public string Description {get; set;}

        [Required]
        public int BusinesId {get; set;}
    }

    public class UpdateRolesDto
    {
        [Required]
        public int Id {get; set;}

        [Required]
        public string Title {get; set;}

        public string Description {get; set;}

        [Required]
        public int BusinesId {get; set;}
    }

    public class DeleteRolesDto
    {
        [Required]
        public int Id {get; set;}
    }
}