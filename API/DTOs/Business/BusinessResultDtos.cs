using API.Entities;
using API.UsersDTOs;

namespace API.DTOs.Business
{
    public class BusinessResultDtos
    {
        public int Id {get; set;}

        public ResultAllUserDto User {get; set;}

        public string Name {get; set;}

        public string Description {get; set;}

        public string Address {get; set;}

        public string Country {get; set;}

        public string City {get; set;}

        public string Lng {get; set;}

        public string Lat {get; set;}

        public int CountryCode {get; set;}

        public int PhoneNumber {get; set;}

        public string Email {get; set;}

        public string Reference {get; set;}

        public int Status {get; set;}
        
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class DeleteBusinessResultDto
    {
        public Boolean Status { get; set; }
        public string Message { get; set; }
    }
}