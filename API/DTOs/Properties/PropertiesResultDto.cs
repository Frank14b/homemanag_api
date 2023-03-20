namespace API.DTOs.Properties
{
    public class PropertiesResultDto
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Reference {get; set;}
        public string ShortDesc {get; set;}
        public string Description {get; set;}
        public string Address {get; set;}
        public string Country {get; set;}
        public string City {get; set;}
        public string Lng {get; set;}
        public string Lat {get; set;}
        public string slug {get; set;}
        public int Status {get; set;}
        public int BusinessId {get; set;}
        public int UserId {get; set;}
        public int PropertyTypeId {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
    public class PropertiesDeleteDto
    {
        public Boolean status {get; set;}
        public String Message {get; set;}
    }

    public class TotalPropertiesDto
    {
        public int active {get; set;}
        public int inactive {get; set;}
    }
}