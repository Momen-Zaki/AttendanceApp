using AttendanceApp.WebApi.Entities;
using AttendanceApp.WebApi.Models;
using FastEndpoints;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class GetAllMapper : ResponseMapper<GetAllResponse, IEnumerable<User>>
    {
        public override Task<GetAllResponse> FromEntityAsync(
            IEnumerable<User> entities, CancellationToken ct = default)
        {
            var response = new GetAllResponse();
            foreach (var e in entities)
            {
                response.Users.Append(new UserDto()
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    UserName = e.UserName,
                    //PasswrodHash = e.PasswrodHash,
                });
            }
            return Task.FromResult(response);
        }
    }
}