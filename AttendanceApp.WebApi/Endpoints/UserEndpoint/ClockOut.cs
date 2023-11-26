using AttendanceApp.WebApi.Entities;
using AttendanceApp.WebApi.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
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
        private readonly IAttendanceRepository _repository;

        public ClockOut(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Get("users/{Id:Guid}/clockout");
            Roles();
            Description(x => x.WithName("ClockOut"));
        }   
                    
        public override async Task HandleAsync(ClockOutRequest req, CancellationToken ct)
        {
            var routeId = Route<Guid>("Id");

            if (routeId.ToString() != req.Id)
                ThrowError("Unauthorized");

            var user = await _repository.GetUserByIdAsync(routeId);
            if (user == null)
                ThrowError("user not found");

            var attendanceForToday =
                await _repository.GetAttendanceRecordForTodayByUserIDAsync(routeId);

            if (attendanceForToday == null
                || (attendanceForToday != null && attendanceForToday.ClockedIn == false))
            {
                ThrowError("pleas clock-in first");
            }
            else
            {
                attendanceForToday.ClockedOut = true;
                attendanceForToday.ClockedOutAt = DateTime.Now;
                await _repository.SaveChangesAsync();
            }


            Response.messege = "See u tommorrow";
            await SendAsync(Response, cancellation: ct);
        }
    }
}
