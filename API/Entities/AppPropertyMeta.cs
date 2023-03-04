using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppPropertyMeta
    {
        public int Id {get; set;}

        [Required]
        [MinLength(3)]
        public string MetaKey {get; set;}

        public string MetaValue {get; set;}
        public string MetaCustom {get; set;}
        public Boolean MetaUnique {get; set;} = false;
        
        [EnumDataType(typeof(StatusEnum))]
        public int Status {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

        public int PropertyId {get; set;}
        public AppProperty Property {get; set;}
    }
}