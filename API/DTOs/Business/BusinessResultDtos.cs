
namespace API.DTOs.Business
{
    public class BusinessResultDtos
    {
        public int Id {get; set;}

        public string Title {get; set;}

        public string Code {get; set;}

        public string Description {get; set;}

        public int Status {get; set;}

        public DateTime CreatedAt {get; set;}
    }

    public class DeleteBusinessResultDto
    {
        public Boolean Status { get; set; }
        public string Message { get; set; }
    }
}