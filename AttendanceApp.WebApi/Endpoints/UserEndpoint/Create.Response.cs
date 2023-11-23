using AttendanceApp.WebApi.Models;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class CreateResponse
    {
        public UserWithoutAttendanceDto User { get; set; }
    }
}