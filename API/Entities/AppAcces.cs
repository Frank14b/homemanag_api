
namespace API.Entities
{
    public class AppAcces
    {
        public int Id {get; set;}

        public string Name {get; set;}

        public string Description {get; set;}

        public string MiddleWare {get; set;}

        public string ApiPath {get; set;}

        public int Status {get; set;} = (int)StatusEnum.enable;

        public List<AppRoleAcces> RoleAcces {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
    }
}