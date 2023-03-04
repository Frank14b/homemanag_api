using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppPropertyMedia
    {
        public int Id {get; set;}

        [Required]
        [MinLength(3)]
        public string Name {get; set;}
        public string link {get; set;}
        public string publicId {get; set;}
        
        [EnumDataType(typeof(MediaType))]
        public int Type {get; set;}
        
        [EnumDataType(typeof(StatusEnum))]
        public int Status {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        
        public int PropertyId {get; set;}
        public AppProperty Property {get; set;}
    }

    public enum MediaType 
    {
        image = 1,
        video = 2,
        pdf = 3,
    }
}