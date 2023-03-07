using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<AppUser> Users {get; set;}

        public DbSet<AppAcces> Access {get; set;}

        public DbSet<AppRole> Roles {get; set;}

        public DbSet<AppRoleAcces> Roleaccess {get; set;}

        public DbSet<AppBusiness> Business {get; set;}

        public DbSet<AppProperty> Properties {get; set;}

        public DbSet<AppPropertyMedia> PropertyMedias {get; set;}

        public DbSet<AppPropertyMeta> PropertyMetas {get; set;}

        public DbSet<AppPropertyType> PropertyTypes {get; set;}

        public DbSet<AppUserProperty> UserProperties {get; set;}
    }
}