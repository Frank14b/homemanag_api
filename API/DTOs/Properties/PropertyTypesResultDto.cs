namespace API.DTOs.Properties
{
    public class PropertyTResultListDto
    {
        public int Id {get; set;}
        public int SubTypeId {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public int Status {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public PropertyTResultListDto SubTypes {get; set;}
    }
    public class PropertyTDeleteDto
    {
        public Boolean Status {get; set;}
        public String Message {get; set;}
    }

    public class PropertyTResultDto
    {
        public IEnumerable<PropertyTResultListDto> Data {get; set;}
        public int Total {get; set;}
        public int Skip {get; set;}
        public int Limit {get; set;}
    }
}