
namespace API.Entities
{
    public class AppUser
    {
        public int Id {get; set;}

        public string UserName {get; set;}

        public string FirstName {get; set;}

        public string LastName {get; set;}

        public int Status {get; set;}

        public string Email {get; set;}

        public string Password {get; set;}

        public int Role {get; set;}

        public DateTime LastLogin {get; set;} 

        public DateTime CreatedAt {get; set;}

        public DateTime UpdatedAt {get; set;}
    }
}
