using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppPropertyType
    {
        public int Id {get; set;}

        [Required]
        [MinLength(3)]
        public string Title {get; set;}
        public int SubTypeId {get; set;}
        public string Description {get; set;}
        
        [EnumDataType(typeof(StatusEnum))]
        public int Status {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

        public List<AppProperty> Properties {get; set;} = new();
    }
}