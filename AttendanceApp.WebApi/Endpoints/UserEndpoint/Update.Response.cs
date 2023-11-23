using AttendanceApp.WebApi.Models;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class UpdateResponse
    {
        public UserWithoutAttendanceDto User { get; set; } = new UserWithoutAttendanceDto();
    }
}