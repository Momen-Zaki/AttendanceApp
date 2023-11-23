using AttendanceApp.WebApi.Models;
using System.Collections.Generic;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class GetAllResponse
    {
        //public string message { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }
}