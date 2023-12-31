﻿using AttendanceApp.WebApi.Entities;
using System;
using System.Collections.Generic;

namespace AttendanceApp.WebApi.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        //public string PasswrodHash { get; set; }
        public string Role { get; set; }
        public ICollection<Attendance> AttendanceRecords { get; set; }
            = new List<Attendance>();
    }
}
