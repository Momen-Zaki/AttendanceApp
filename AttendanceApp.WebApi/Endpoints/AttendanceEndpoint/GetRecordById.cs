﻿using AttendanceApp.WebApi.Endpoints.UserEndpoint;
using AttendanceApp.WebApi.Entities;
using AttendanceApp.WebApi.Models;
using AttendanceApp.WebApi.Services;
using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Endpoints.AttendanceEndpoint
{
    public class GetRecordById : EndpointWithoutRequest<GetRecordByIdResponse, GetRecordByIdMappeer>
    {
        private readonly IAttendanceRepository _repositoy;

        public GetRecordById(IAttendanceRepository repositoy)
        {
            _repositoy = repositoy;
        }

        public override void Configure()
        {
            Get("users/{Id:Guid}/attendance/{recordId:Guid}");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Get Attendance Record of a User by Record Id";
                s.Description = "Returns an Attendance Record of a User by Record Id";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var userId = Route<Guid>("Id");
            var recordId = Route<Guid>("recordId");
            var user = await _repositoy.GetUserByIdAsync(userId);
            if (user == null)
                ThrowError("user not found");

            var attendancRecord =
                await _repositoy.GetAttendanceRecordByIdAsync(recordId);
            if (attendancRecord == null)
                ThrowError("attendance record is not found");

            Response = Map.FromEntity(attendancRecord);
            await SendAsync(Response);
        }
    }
}
