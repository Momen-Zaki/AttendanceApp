using AttendanceApp.WebApi.Models;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class GetUserWithRecordsByIdResponse
    {
        public UserDto User { get; set; } = new UserDto();
    }
}