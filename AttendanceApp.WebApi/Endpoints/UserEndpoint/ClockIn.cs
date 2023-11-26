using AttendanceApp.WebApi.Entities;
using AttendanceApp.WebApi.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class ClockIn : Endpoint<ClockInRequest, ClockInResponse>
    {
        private readonly IAttendanceRepository _repository;

        public ClockIn(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Get("users/{Id:Guid}/clockin");
            Roles();
            Description(x => x.WithName("ClockIn"));
        }

        public override async Task HandleAsync(ClockInRequest req, CancellationToken ct)
        {
            var routeId = Route<Guid>("Id");

            if (routeId.ToString() != req.Id)
                ThrowError("Unauthorized");

            var user = await _repository.GetUserByIdAsync(routeId);
            if (user == null)
                ThrowError("user not found");

            var attendanceForToday =
                await _repository.GetAttendanceRecordForTodayByUserIDAsync(routeId);
            if (attendanceForToday == null)
            {
                var newAttendance = new Attendance()
                {
                    AttendanceDay = DateTime.Now,
                    ClockedIn = true,
                    ClockedInAt = DateTime.Now,
                    ClockedOut = false,
                    ClockedOutAt = new DateTime()
                };
                await _repository.AddAttendanceRecord(newAttendance, routeId);
            }
            else
            {
                attendanceForToday.ClockedIn = true;
                attendanceForToday.ClockedInAt = DateTime.Now;
            }


            Response.messege = "have a nice day";
            await SendAsync(Response, cancellation: ct);
        }
    }
}
