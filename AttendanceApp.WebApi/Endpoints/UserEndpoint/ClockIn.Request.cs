using FastEndpoints;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class ClockInRequest
    {
        [FromClaim] public string Id { get; set; }
    }
}