using AttendanceApp.WebApi.Models;
using AttendanceApp.WebApi.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class GetById : EndpointWithoutRequest<GetByIdResponse, GetByIdMapper>
    {
        private readonly IAttendanceRepository _repository;

        public GetById(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Get("users/{Id:Guid}");
            Roles("Admin");
            Description(x => x.WithName("GetUserById"));
            Summary(s =>
            {
                s.Summary = "Get User by Id";
                s.Description = "Return a User with the give id if it exists";
                s.ResponseExamples[200] = new GetByIdResponse
                {
                    User = new UserWithoutAttendanceDto()
                    {
                        Id = Guid.NewGuid(),
                        FullName = string.Empty,
                        UserName = string.Empty,
                        Role = UserRole.Employee
                    }
                };
                s.Responses[200] = "ok with the user data";
                s.Responses[404] = "Can't find a user with this Id";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            Guid userId = Route<Guid>("Id");
            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null)
            {
                await SendNoContentAsync();
            }
            Response = Map.FromEntity(user);
            await SendAsync(Response);
        }
    }
}
