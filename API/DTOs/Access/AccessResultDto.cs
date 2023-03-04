namespace API.AccessDTOs
{
    public class AccessResultDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MiddleWare { get; set; }

        // public string ApiPath { get; set; }
        
        public int Status { get; set; }
    }

    public class AccessListResultDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MiddleWare { get; set; }

        // public string ApiPath { get; set; }

        public int Status { get; set; }
    }

    public class DeleteAccessResultDto
    {
        public Boolean Status { get; set; }
        public string Message { get; set; }
    }

}