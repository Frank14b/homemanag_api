namespace API.DTOs.Properties
{
    public class PropertyMTResultDto
    {
        public int Id {get; set;}
        public string MetaKey {get; set;}
        public string MetaValue {get; set;}
        public string MetaCustom {get; set;}
        public Boolean MetaUnique {get; set;}
        public int Status {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public int PropertyId {get; set;}
    }
    public class PropertyMTDeleteDto
    {
        public Boolean status {get; set;}
        public String Message {get; set;}
    }
}