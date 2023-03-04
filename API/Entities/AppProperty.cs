using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppProperty
    {
        public int Id {get; set;}
        
        [Required]
        [MinLength(3)]
        public string Name {get; set;}
        
        [Required]
        public string Reference {get; set;}

        [Required]
        [MinLength(3)]
        public string ShortDesc {get; set;}

        [Required]
        [MinLength(3)]
        public string Description {get; set;}
        
        [EnumDataType(typeof(StatusEnum))]
        public int Status {get; set;}
        public int BusinessId {get; set;}
        public AppBusiness Business {get; set;}
        public int UserId {get; set;}
        public AppUser User {get; set;}
        public int PropertyTypeId {get; set;}
        public AppPropertyType PropertyType {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

        public List<AppPropertyMedia> PropertyMedias {get; set;} = new();
        public List<AppPropertyMeta> PropertyMetas {get; set;} = new();
        public List<AppUserProperty> UserProperties {get; set;} = new();
    }
}