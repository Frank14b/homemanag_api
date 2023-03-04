
using API.Entities;

namespace API.DTOs.Roles
{
    public class RoleResultDtos
    {
        public int Id {get; set;}

        public string Title {get; set;}

        public string Code {get; set;}

        public string Description {get; set;}

        public int Status {get; set;}

        public AppBusiness Business {get; set;}

        public DateTime CreatedAt {get; set;}
    }

    public class DeleteRoleResultDto
    {
        public Boolean Status { get; set; }
        public string Message { get; set; }
    }
}