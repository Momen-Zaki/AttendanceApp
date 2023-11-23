using AttendanceApp.WebApi.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class GetUserWithRecordsById
        : EndpointWithoutRequest<GetUserWithRecordsByIdResponse, GetUserWithRecordsByIdMapper>
    {
        private readonly IAttendanceRepository _repository;

        public GetUserWithRecordsById(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Get("users/{Id:Guid}/withrecords");
            AllowAnonymous();
            Description(x => x.WithName("GetUserWithRecordsById"));
            Summary(s =>
            {
                s.Summary = "Get User by Id with All his Attendance Record";
                s.Description = "Return a User with All his Attendance " +
                    "Record by the give id if it exists";
                //s.ExampleRequest = new MyRequest { ...};
                //s.ResponseExamples[200] = new MyResponse { ...};
                //s.Responses[200] = "ok response description goes here";
                //s.Responses[404] = "Can't find a user with this Id";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var userId = Route<Guid>("Id");
            var user = await _repository.GetUserByIdAsync(userId, true);
            if (user == null)
            {
                await SendNoContentAsync();
            }
            Response = Map.FromEntity(user);
            await SendAsync(Response);
        }
    }
}
