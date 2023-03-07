namespace API.DTOs.Properties
{
    public class PropertyTResultDto
    {
        public int Id {get; set;}
        public int SubTypeId {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public int Status {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
    public class PropertyTDeleteDto
    {
        public Boolean status {get; set;}
        public String Message {get; set;}
    }
}