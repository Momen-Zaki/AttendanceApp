﻿using AttendanceApp.WebApi.Models;

namespace AttendanceApp.WebApi.Endpoints.UserEndpoint
{
    public class CreateRequest
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = UserRole.Employee;
    }
}