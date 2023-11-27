using AttendanceApp.WebApi.Models;

namespace AttendanceApp.WebApi.Endpoints.AttendanceEndpoint
{
    public class GetRecordByIdResponse
    {
        public AttendanceDto Record { get; set; }
    }
}
