using AttendanceApp.WebApi.Endpoints.UserEndpoint;
using AttendanceApp.WebApi.Entities;
using AttendanceApp.WebApi.Services;
using FastEndpoints;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.AttendanceEndpoint
{
    public class CreateForUser 
        : Endpoint<CreateForUserRequest, CreateForUserResponse, CreateForUserMapper>
    {
        private readonly IAttendanceRepository _repository;

        public CreateForUser(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Post("users/{Id:Guid}/attendance");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Create a new Attendance Record for a User";
                s.Description = "Create a new Attendance Record for a User by his Id";
            });
        }

        public override async Task HandleAsync(CreateForUserRequest req, CancellationToken ct)
        {
            var userId = Route<Guid>("Id");

            if (!await _repository.UserExistsWithId(userId))
                ThrowError("user not found");

            var newAttendance = Map.ToEntity(req);
            newAttendance = await _repository.AddAttendanceRecord(newAttendance, userId);
            
            //try
            //{
                //await _repository.SaveChangesAsync();

            //}
            //catch (Exception e)
            //{
            //    await _repository.ReloadUserAsync(user);
            //    await _repository.SaveChangesAsync();
            //    ThrowError(e.Message);
            //}

            Response = Map.FromEntity(newAttendance);
            await Task.FromResult(Response);
            //await SendCreatedAtAsync<GetById>("GetAttendanceById",Response);
        }
    }
}
