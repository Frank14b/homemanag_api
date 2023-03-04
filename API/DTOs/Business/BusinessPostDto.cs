
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Business
{
    public class CreateBusinessDto
    {
        [Required]
        [MinLength(3)]
        public string Name {get; set;}

        public string Description {get; set;}

        [Required]
        public string Address {get; set;}

        [Required]
        public string Country {get; set;}

        [Required]
        public string City {get; set;}

        public string Lng {get; set;}

        public string Lat {get; set;}

        [Required]
        public int CountryCode {get; set;}

        [Required]
        public int PhoneNumber {get; set;}

        [EmailAddress]
        public string Email {get; set;}
        
    }

    public class UpdateBusinessDto
    {
        [Required]
        public int Id {get; set;}

        [MinLength(3)]
        public string Name {get; set;}
        
        public string Description {get; set;}

        [MinLength(3)]
        public string Address {get; set;}

        [MinLength(3)]
        public string Country {get; set;}

        [MinLength(3)]
        public string City {get; set;}

        [MinLength(3)]
        public string Lng {get; set;}

        [MinLength(3)]
        public string Lat {get; set;}

        [MinLength(1)]
        public int CountryCode {get; set;}

        [MinLength(4)]
        public int PhoneNumber {get; set;}

        [MinLength(3)]
        [EmailAddress]
        public string Email {get; set;}
    }

    public class DeleteBusinessDto
    {
        [Required]
        public int Id {get; set;}
    }
}