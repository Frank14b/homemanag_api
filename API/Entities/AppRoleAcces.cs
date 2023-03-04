
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Roleaccess")]
    public class AppRoleAcces
    {
        public int Id {get; set;}

        public int Status {get; set;}
        
        public int AccesId {get; set;}
        public AppAcces Acces {get; set;}

        public int RoleId {get; set;}
        public AppRole Role {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

        public DateTime UpdatedAt {get; set;}
    }
}