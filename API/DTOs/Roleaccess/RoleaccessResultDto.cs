using API.AccessDTOs;
using API.DTOs.Roles;

namespace API.DTOs.Roleaccess
{
    public class RoleaccessResultDto
    {
        public int Id {get; set;}

        public int Status {get; set;}

        public int RoleId {get; set;}
        public RoleResultDtos Role {get; set;}

        public int AccesId {get; set;}
        public AccessResultDto Acces {get; set;}

        public DateTime CreatedAt {get; set;}

        public DateTime UpdatedAt {get; set;}
    }
}