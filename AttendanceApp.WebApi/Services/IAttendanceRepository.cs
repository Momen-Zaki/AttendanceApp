using AttendanceApp.WebApi.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Services
{
    public interface IAttendanceRepository
    {
        // User Methods
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<User> GetUserByIdAsync(Guid id, bool includeAttendance = false);
        public Task<User> FindUserByIdAsync(Guid id);
        public Task<User> GetUserByUserNameAsync(string userName);
        public Task<bool> UserExistsWithId(Guid id);
        public Task<bool> UserExistsWithUserName(string username);
        public Task<bool> DeleteUserAsync(User user);

        // Attendace Record Methods
        public Task<IEnumerable<Attendance>> GetAllAttendanceRecordsAsync();
        public Task<IEnumerable<Attendance>> GetAllAttendanceRecordsByUserIdAsync(Guid id);
        public Task<Attendance> GetAttendanceRecordByIdAsync(Guid id);
        public Task<bool> AttendanceRecordExists(Guid id);
        public Task<bool> DeleteAttendanceRecordAsync(Guid id);

        // SaveChanegs
        public Task<bool> SaveChangesAsync();
        public Task<User> ReloadUserAsync(User user);
        public Task<Attendance> ReloadAttendanceRecordAsync(Attendance record);
        Task<Attendance> AddAttendanceRecord(Attendance newAttendance, Guid userId);
    }
}
