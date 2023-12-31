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
            Roles("Admin");
            Summary(s =>
            {
                s.Summary = "Delete User by Id";
                s.Description = "Delete a User with the give id if it exists";
                s.ResponseExamples[200] = new DeleteResponse { Message = "user deleted!"};
                s.Responses[200] = "ok with a confirmation message";
                s.Responses[404] = "Can't delete it for now";
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
            await SendAsync(new() { Message = "user deleted!" }, cancellation: ct);
        }
    }
}
