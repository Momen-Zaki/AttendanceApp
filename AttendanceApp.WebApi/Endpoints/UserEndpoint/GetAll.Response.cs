using AttendanceApp.WebApi.Models;
using System.Collections.Generic;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class GetAllResponse
    {
        public IEnumerable<UserWithoutAttendanceDto> Users { get; set; }
    }
}