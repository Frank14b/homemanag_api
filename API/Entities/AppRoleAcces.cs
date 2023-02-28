
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppRoleAcces
    {
        public int Id {get; set;}

        public int Status {get; set;}

        [Required]
        public AppAcces Acces {get; set;}

        [Required]
        public AppRole Role {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

        public DateTime UpdatedAt {get; set;}
    }
}