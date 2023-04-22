
using API.DTOs.Business;

namespace API.UsersDTOs
{
    public class ResultloginDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Token {get; set;}
    }

    public class ResultDeleteUserDto
    {
        public Boolean Status {get; set;}

        public string Message {get; set;}
    }

    public class ResultUpdateUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ResultAllUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BusinessResultListDto> Business {get; set;}
    }

    public class TotalUsersDto
    {
        public int employees {get; set;}
        public int all {get; set;}
    }
}