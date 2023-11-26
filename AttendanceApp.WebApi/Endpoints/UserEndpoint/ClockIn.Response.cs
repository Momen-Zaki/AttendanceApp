using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class ClockInResponse
    {
        public IEnumerable<Claim> claims { get; set; } = new List<Claim>();
    }
}