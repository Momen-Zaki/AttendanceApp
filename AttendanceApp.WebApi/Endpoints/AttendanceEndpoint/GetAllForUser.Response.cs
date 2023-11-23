using AttendanceApp.WebApi.Entities;
using AttendanceApp.WebApi.Models;
using System.Collections.Generic;

namespace AttendanceApp.WebApi.Endpoints.AttendanceEndpoint
{
    public class GetAllForUserResponse
    {
        public IEnumerable<AttendanceDto> AttendanceList { get; set; }
                = new List<AttendanceDto>();
    }
}
