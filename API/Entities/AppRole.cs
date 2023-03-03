using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppRole
    {
        public int Id {get; set;}

        [Required]
        [MinLength(3)]
        public string Title {get; set;}

        [Required]
        [MinLength(3)]
        public string Code {get; set;}

        public string Description {get; set;}

        public int Status {get; set;}

        [Required]
        public AppBusiness Busines {get; set;}

        public List<AppRoleAcces> RoleAcces {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
    }
}