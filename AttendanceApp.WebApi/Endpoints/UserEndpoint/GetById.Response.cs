using AttendanceApp.WebApi.Models;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class GetByIdResponse
    {
        public UserWithoutAttendanceDto User { get; set; } = new UserWithoutAttendanceDto();
    }
}