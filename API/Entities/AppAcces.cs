
namespace API.Entities
{
    public class AppAcces
    {
        public int Id {get; set;}

        public string Name {get; set;}

        public string Description {get; set;}

        public string MiddleWare {get; set;}

        public string ApiPath {get; set;}

        public string Status {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    }
}