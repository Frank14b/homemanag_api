using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Properties
{
    public class PropertyTCreateDto
    {
        public int SubTypeId {get; set;}

        [Required]
        [MinLength(3)]
        public string Name {get; set;}
        public string Description {get; set;}
    }

    public class PropertyTUpdateDto
    {
        [Required]
        public int Id {get; set;}
        public int SubTypeId {get; set;}

        [Required]
        [MinLength(3)]
        public string Name {get; set;}
        public string Description {get; set;}
    }
}