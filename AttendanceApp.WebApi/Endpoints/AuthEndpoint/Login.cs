using AttendanceApp.WebApi.Models;
using AttendanceApp.WebApi.Services;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.AuthEndpoint
{
    public class Login : Endpoint<LoginRequest, LoginResponse>
    {
        private readonly IConfiguration configuration;
        private readonly IAttendanceRepository _repository;

        public Login(IConfiguration configuration, IAttendanceRepository repository)
        {
            this.configuration = configuration;
            _repository = repository;
        }

        public override void Configure()
        {
            Post("login");
            AllowAnonymous();
            Description(x => x.WithName("Login"));
            Summary(s =>
            {
                s.Summary = "Login";
                s.Description = "Return a Token for the give username and password";
                //s.ExampleRequest = new MyRequest { ...};
                //s.ResponseExamples[200] = new MyResponse { ...};
                //s.Responses[200] = "ok response description goes here";
                //s.Responses[404] = "Can't find a user with this Id";
            });
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            var user = await _repository.GetUserByUserNameAsync(req.Username);
            bool passwordMatches = BCrypt.Net.BCrypt.Verify(req.Password, user.PasswrodHash);

            if (user == null || !passwordMatches)
                ThrowError("The supplied credentials are invalid!");

            string token = string.Empty;
            if (user.Role == UserRole.Admin)
            {
                token = JWTBearer.CreateToken(
                    signingKey: configuration["JwtBearerDefaults:SecretKey"],
                    expireAt: DateTime.UtcNow.AddDays(1),
                    privileges: u =>
                    {
                        u.Roles.Add("Admin");
                        u.Claims.Add(
                            new Claim("UserName", user.UserName),
                            new Claim("Role", user.Role),
                            new Claim("Id", user.Id.ToString())
                            );
                    });
            }
            else
            {
                token = JWTBearer.CreateToken(
                    signingKey: configuration["JwtBearerDefaults:SecretKey"],
                    expireAt: DateTime.UtcNow.AddDays(1),
                    privileges: u =>
                    {
                        u.Roles.Add("Employee");
                        u.Claims.Add(
                            new Claim("UserName", user.UserName),
                            new Claim("Role", user.Role),
                            new Claim("Id", user.Id.ToString())
                            );
                    });
            }

            Response.UserName = req.Username;
            Response.Token = token;
            await SendAsync(Response, cancellation: ct);
        }
    }
}
