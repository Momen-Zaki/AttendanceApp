using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class ClockOut : Endpoint<ClockOutRequest, ClockOutResponse>
    {
        public override void Configure()
        {
            Post("users/{Id:Guid}/clockout");
        }

        public override async Task HandleAsync(ClockOutRequest req, CancellationToken ct)
        {
            var userId = Route<Guid>("Id");

            // 1- Make Sure He is the same user as the userId he tries to acces
            // 2- IF there is no AttendanceRecord for today SendError(" ClockIn First")
            //    IF He has ClockedIn get the AttendanceRecord and Set ClockedOut = true
            //    and ClockedOutAt = now

            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = claimsIdentity.Claims;
            Response.claims = claims;
            await SendAsync(Response, cancellation: ct);
        }
    }
}
