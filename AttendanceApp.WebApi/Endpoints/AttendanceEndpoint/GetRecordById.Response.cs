using AttendanceApp.WebApi.Entities;
using AttendanceApp.WebApi.Models;
using System.Collections.Generic;

namespace AttendanceApp.WebApi.Endpoints.AttendanceEndpoint
{
    public class GetRecordByIdResponse
    {
        public AttendanceDto Record { get; set; }
    }
}
