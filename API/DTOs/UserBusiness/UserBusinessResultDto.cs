using API.Entities;
using API.UsersDTOs;
using API.DTOs.Business;

namespace API.DTOs.UserBusiness
{
    public class UserBusinessResultDto
    {
        public int Id {get; set;}
        public int Status {get; set;}
        
        public int UserId {get; set;}
        public ResultAllUserDto User {get; set;}

        public int BusinesId {get; set;}
        public BusinessResultListDto Business {get; set;}

        public Boolean IsAdmin {get; set;}

        public int CreatedBy {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }

    public class UserBusinessDeleteDto {
        public Boolean Status {get; set;}
        public String Message {get; set;}
    }
}