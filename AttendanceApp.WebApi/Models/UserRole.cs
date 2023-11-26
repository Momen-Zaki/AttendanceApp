namespace AttendanceApp.WebApi.Models
{
    public static class UserRole
    {
        public const string Admin = "admin";
        public const string Employee = "employee";
    }
    
    public enum Roles
    {
        Employee,
        Admin,
    }
}
