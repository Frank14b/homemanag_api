using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Properties
{
    public class PropertyMTCreateDto
    {
        [Required]
        [MinLength(3)]
        public string MetaKey {get; set;}

        public string MetaValue {get; set;}
        public string MetaCustom {get; set;}

        [Required]
        public Boolean MetaUnique {get; set;}
        public int PropertyId {get; set;}
    }

    public class PropertyMTUpdateDto
    {
        [Required]
        public int Id {get; set;}

        [Required]
        [MinLength(3)]
        public string MetaKey {get; set;}

        public string MetaValue {get; set;}
        public string MetaCustom {get; set;}

        [Required]
        public Boolean MetaUnique {get; set;}
        
        public int PropertyId {get; set;}
    }
}