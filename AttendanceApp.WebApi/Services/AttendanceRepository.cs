using AttendanceApp.WebApi.DbContexts;
using AttendanceApp.WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceApp.WebApi.Services
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AttendanceContext _context;

        // USER Methods
        public AttendanceRepository(AttendanceContext cotext)
        {
            _context = cotext;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users =  await _context.Users.ToListAsync();
            await Console.Out.WriteLineAsync(users[0].FullName);
            return users;
        }

        public async Task<User> GetUserByIdAsync(Guid id, bool includeAttendance = false)
        {
            if (includeAttendance) 
                return await _context.Users.Include(u => u.AttendanceRecords)
                    .Where(u => u.Id == id)
                    .FirstOrDefaultAsync();

            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<User> FindUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> UserExistsWithIdAsync(Guid id)
        {
            return !(await _context.Users.FirstOrDefaultAsync(u => u.Id == id) == null);
        }

        public async Task<bool> UserExistsWithUserNameAsync(string username)
        {
            return !(await _context.Users.FirstOrDefaultAsync
                            (u => u.UserName == username) == null);
        }
        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Remove(user);
            return await this.SaveChangesAsync();
        }
        
        // SaveChanges
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        // Attendace Record Methods
        public async Task<IEnumerable<Attendance>> 
            GetAllAttendanceRecordsByUserIdAsync(Guid id)
        {
            var user = await this.GetUserByIdAsync(id, true);
            return  user.AttendanceRecords.ToList();
        }

        public Task<Attendance> GetAttendanceRecordByIdAsync(Guid id)
        {
            var AttendanceRecord =
                _context.AttenanceRecords.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(AttendanceRecord);
        }

        public Task<bool> AttendanceRecordExists(Guid id)
        {
            var AttendanceRecord =
                _context.AttenanceRecords.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(AttendanceRecord != null);
        }

        public async Task<bool> DeleteAttendanceRecordAsync(Guid id)
        {
            var AttendanceRecordToBeDeleted =
                _context.AttenanceRecords.FirstOrDefault(u => u.Id == id);

            var AttendanceRecords = _context.AttenanceRecords;
            AttendanceRecords.Remove(AttendanceRecordToBeDeleted);

            return await this.SaveChangesAsync();
        }

        public async Task<IEnumerable<Attendance>> GetAllAttendanceRecordsAsync()
        {
            return await _context.AttenanceRecords.ToListAsync();
        }

        public async Task<Attendance> AddAttendanceRecord(
                Attendance newAttendance, Guid userId)
        {
            newAttendance.User = await _context.Users.FindAsync(userId);
            await _context.AttenanceRecords.AddAsync(newAttendance);
            await this.SaveChangesAsync();
            return await Task.FromResult(newAttendance);
        }

        public Task<Attendance> GetAttendanceRecordForTodayByUserIDAsync(Guid id)
        {
            var Record = _context.AttenanceRecords
                            .Where(a => a.UserId == id)
                            .Where(a => a.AttendanceDay.Date == DateTime.Today)
                            .FirstOrDefault();

            return Task.FromResult(Record);
        }
    }
}
