using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppPropertyType
    {
        public int Id {get; set;}

        public int SubTypeId {get; set;}

        [Required]
        [MinLength(3)]
        public string Name {get; set;}
        public string Description {get; set;}
        
        [EnumDataType(typeof(StatusEnum))]
        [DefaultValue(StatusEnum.enable)]
        public int Status {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

        public List<AppProperty> Properties {get; set;} = new();
    }
}