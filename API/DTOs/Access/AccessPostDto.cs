
using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.AccessDTOs
{
    public class CreateAccessDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string MiddleWare { get; set; }

        public string ApiPath { get; set; }
    }

    public class UpdateAccessDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string MiddleWare { get; set; }

        public string ApiPath { get; set; }

        public int Status { get; set; } = (int)StatusEnum.enable;
    }

    public class DeleteAccessDto
    {
        [Required]
        public int Id { get; set; }
    }
}