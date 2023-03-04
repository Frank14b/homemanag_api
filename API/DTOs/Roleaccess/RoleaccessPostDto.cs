using System.ComponentModel.DataAnnotations;
using API.AccessDTOs;

namespace API.DTOs.Roleaccess
{
    public class RoleaccessPostDto
    {
        [Required]
        public int RoleId {get; set;}

        [Required]
        public int AccesId {get; set;}
    }
}