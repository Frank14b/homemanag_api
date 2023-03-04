using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Roles")]
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
        public int BusinessId {get; set;}
        public AppBusiness Business {get; set;}

        public List<AppRoleAcces> RoleAcces {get; set;} = new();

        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
    }
}