using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        [MinLength(3)]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EnumDataType(typeof(StatusEnum))]
        public int Status { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [EnumDataType(typeof(RoleEnum))]
        public int Role { get; set; }

        public DateTime LastLogin { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
    }

    public enum RoleEnum
    {
        user = 1,
        admin = 2
    }

    public enum StatusEnum
    {
        disable = 0,
        enable = 1,
        delete = 2
    }
}
