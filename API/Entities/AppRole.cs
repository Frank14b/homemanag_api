using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Roles")]
    public class AppRole
    {
        public int Id {get; set;}

        public string Title {get; set;}

        public string Code {get; set;}

        public string Description {get; set;}

        public int Status {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    }
}