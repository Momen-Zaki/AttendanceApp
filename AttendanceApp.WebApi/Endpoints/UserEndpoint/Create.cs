﻿using AttendanceApp.WebApi.DbContexts;
using AttendanceApp.WebApi.Entities;
using AttendanceApp.WebApi.Services;
using FastEndpoints;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class Create : Endpoint<CreateRequest, CreateResponse, CreateMapper>
    {
        private readonly AttendanceContext _context;
        private readonly IAttendanceRepository _reposityory;

        public Create(AttendanceContext context, IAttendanceRepository reposityory)
        {
            _context = context;
            _reposityory = reposityory;
        }

        public override void Configure()
        {
            Post("/users/create");
            //AllowAnonymous();
            Roles("Admin");
        }

        public override async Task HandleAsync(CreateRequest req, CancellationToken ct)
        {

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(req.Password);

            var newUser = new User()
            {
                FullName = req.FullName,
                UserName = req.UserName,
                PasswrodHash = hashedPassword,
                Role = req.Role,
            };
            _context.Users.Add(newUser);
            await _reposityory.SaveChangesAsync();

            var userCreated = _context.Users
                .FirstOrDefault(u => u.UserName == newUser.UserName);
            if (userCreated == null)
                ThrowError("can't create a new user for now");

            Response = Map.FromEntity(userCreated);
            await SendCreatedAtAsync<GetById>("GetUserById", Response);
        }
    }
}