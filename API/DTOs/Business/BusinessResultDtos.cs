using API.UsersDTOs;

namespace API.DTOs.Business
{

    public class BusinessResultListDto
    {
        public int Id {get; set;}

        public int UserId {get; set;}

        public ResultAllUserDto User {get; set;}

        public string Name {get; set;}

        public string Description {get; set;}

        public string Address {get; set;}

        public string Country {get; set;}

        public string City {get; set;}

        public int Lng {get; set;}

        public int Lat {get; set;}

        public int CountryCode {get; set;}

        public int PhoneNumber {get; set;}

        public string Email {get; set;}

        public string Reference {get; set;}

        public int Status {get; set;}
        
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class BusinessResultDto 
    {
        public IEnumerable<BusinessResultListDto> Data {get; set;}
        public int Total {get; set;}
        public int Skip {get; set;}
        public int Limit {get; set;}
        public string Sort {get; set;}
    }

    public class DeleteBusinessResultDto
    {
        public Boolean Status { get; set; }
        public string Message { get; set; }
    }

    public class TotalBusinessDto
    {
        public int active {get; set;}
        public int inactive {get; set;}
    }
}