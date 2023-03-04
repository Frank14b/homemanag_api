
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.DTOs.Roles;

namespace API.Entities
{
    [Table("Business")]
    public class AppBusiness
    {
        public int Id {get; set;}

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

        [EnumDataType(typeof(StatusEnum))]
        public int Status {get; set;}

        [Required]
        public string Reference {get; set;}
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
        
        public List<RoleResultDtos> Roles {get; set;} = new List<RoleResultDtos>();

        public int UserId {get; set;}
        public AppUser User {get; set;}
    }
}