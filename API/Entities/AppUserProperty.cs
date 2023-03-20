using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppUserProperty
    {
        public int Id {get; set;}

        [EnumDataType(typeof(StatusEnum))]
        [DefaultValue(StatusEnum.enable)]
        public int Status {get; set;}
        
        public int UserId {get; set;}
        public AppUser User {get; set;}

        public int PropertyId {get; set;}
        public AppProperty Property {get; set;}

        public Boolean IsEmployee {get; set;} = false;

        public int RoleId {get; set;}
        public AppRole Role {get; set;}

        public int CreatedBy {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
        public DateTime UpdatedAt {get; set;}
    }
}