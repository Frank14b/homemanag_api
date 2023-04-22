
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Business
{
    public class CreateBusinessDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        public int Lng { get; set; }

        public int Lat { get; set; }

        [Required]
        public int CountryCode { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

    }

    public class UpdateBusinessDto
    {
        [MinLength(3)]
        public string Name { get; set; }

        public string Description { get; set; }

        [MinLength(3)]
        public string Address { get; set; }

        [MinLength(3)]
        public string Country { get; set; }

        public string City { get; set; }
        [Required]
        public int Lng { get; set; }
        [Required]
        public int Lat { get; set; }

        [Required]
        public int CountryCode { get; set; }
        [Required]
        public int PhoneNumber { get; set; }

        [MinLength(3)]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class DeleteBusinessDto
    {
        [Required]
        public int Id { get; set; }
    }
}