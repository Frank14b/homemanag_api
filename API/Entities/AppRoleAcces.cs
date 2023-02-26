
namespace API.Entities
{
    public class AppRoleAcces
    {
        public int Id {get; set;}
        
        public int RoleId {get; set;}

        public int AccesId {get; set;}

        public int Status {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

        public DateTime UpdatedAt {get; set;}
    }
}