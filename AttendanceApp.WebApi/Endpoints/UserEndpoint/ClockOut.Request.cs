using FastEndpoints;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class ClockOutRequest
    {
        [FromClaim] public string Id { get; set; }
    }
}