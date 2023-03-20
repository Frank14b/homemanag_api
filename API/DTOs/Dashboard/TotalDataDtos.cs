using API.DTOs.Business;
using API.DTOs.Properties;
using API.UsersDTOs;

namespace API.DTOs.Dashboard
{
    public class TotalDataDtos
    {
        public TotalBusinessDto business {get; set;}

        public TotalPropertiesDto properties {get; set;}

        public TotalUsersDto users {get; set;}

        public Object maintenances {get; set;}
    }
}