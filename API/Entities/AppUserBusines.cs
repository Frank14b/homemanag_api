using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppUserBusines
    {
        public int Id {get; set;}

        [EnumDataType(typeof(StatusEnum))]
        [DefaultValue(StatusEnum.enable)]
        public int Status {get; set;}
        
        public int UserId {get; set;}
        public AppUser User {get; set;}

        public int BusinesId {get; set;}
        public AppBusiness Business {get; set;}

        public Boolean IsAdmin {get; set;} = false;

        public int CreatedBy {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
        public DateTime UpdatedAt {get; set;}
    }
}