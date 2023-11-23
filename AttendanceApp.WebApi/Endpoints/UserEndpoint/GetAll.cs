using AttendanceApp.WebApi.Models;
using AttendanceApp.WebApi.Services;
using FastEndpoints;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    //public class GetAll : EndpointWithoutRequest<GetAllResponse, GetAllMapper>
    public class GetAll : EndpointWithoutRequest<GetAllResponse>
    {
        private readonly IAttendanceRepository _repository;

        public GetAll(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Get("users");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var entities = await _repository.GetAllUsersAsync();
            var users = new List<UserDto>();
            foreach (var e in entities)
            {
                users.Add(new UserDto()
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    UserName = e.UserName,
                    PasswrodHash = e.PasswrodHash,
                });
            }
            Response.Users = users;
            await SendAsync(Response);
            //Response.Users = Map.FromEntityAsync(entities, ct);
            //await SendMappedAsync(Response);
        }
    }
}
