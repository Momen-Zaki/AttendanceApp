﻿using AttendanceApp.WebApi.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class Delete : EndpointWithoutRequest<DeleteResponse>
    {
        private readonly IAttendanceRepository _repository;

        public Delete(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Delete("users/{Id:Guid}");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Get User by Id";
                s.Description = "Return a User with the give id if it exists";
                //s.ExampleRequest = new MyRequest { ...};
                //s.ResponseExamples[200] = new MyResponse { ...};
                //s.Responses[200] = "ok response description goes here";
                //s.Responses[404] = "Can't find a user with this Id";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            Guid userId = Route<Guid>("Id");
            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null)
            {
                ThrowError("user not found");
            }
            await _repository.DeleteUserAsync(user);
            await SendAsync(new() { message = "user deleted!" }, cancellation: ct);
        }
    }
}
