using AttendanceApp.WebApi.Entities;
using AttendanceApp.WebApi.Models;
using AttendanceApp.WebApi.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class GetUserWithRecordsById
        : EndpointWithoutRequest<GetUserWithRecordsByIdResponse, 
            GetUserWithRecordsByIdMapper>
    {
        private readonly IAttendanceRepository _repository;

        public GetUserWithRecordsById(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Get("users/{Id:Guid}/withrecords");
            Roles("Admin");
            Description(x => x.WithName("GetUserWithRecordsById"));
            Summary(s =>
            {
                s.Summary = "Get User by Id with All his Attendance Record";
                s.Description = "Return a User with All his Attendance " +
                    "Record by the give id if it exists";
                s.ResponseExamples[200] = new GetUserWithRecordsByIdResponse()
                {
                    User = new UserDto()
                    {
                        Id = Guid.NewGuid(),
                        FullName = string.Empty,
                        UserName = string.Empty,
                        Role = UserRole.Employee,
                        AttendanceRecords = new List<Attendance>()
                        {
                            new Attendance()
                            {
                                Id = Guid.NewGuid(),
                                AttendanceDay = new DateTime(),
                                ClockedIn = false,
                                ClockedInAt = new DateTime(),
                                ClockedOut = false,
                                ClockedOutAt = new DateTime(),
                            }
                        }
                    }
                };
                s.Responses[200] = "ok with the user data";
                s.Responses[404] = "Can't find a user with this Id";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var userId = Route<Guid>("Id");
            var user = await _repository.GetUserByIdAsync(userId, true);
            if (user == null)
                ThrowError("user not found");

            Response = Map.FromEntity(user);
            await SendAsync(Response, cancellation: ct);
        }
    }
}
