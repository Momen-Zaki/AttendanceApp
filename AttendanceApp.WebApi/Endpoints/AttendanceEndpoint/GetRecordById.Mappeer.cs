using AttendanceApp.WebApi.Entities;
using AttendanceApp.WebApi.Models;
using FastEndpoints;

namespace AttendanceApp.WebApi.Endpoints.AttendanceEndpoint
{
    public class GetRecordByIdMappeer : ResponseMapper<GetRecordByIdResponse, Attendance>
    {
        public override GetRecordByIdResponse FromEntity(Attendance e) => new() 
        {
            Record = new AttendanceDto()
            {
                Id = e.Id,
                AttendanceDay = e.AttendanceDay,
                ClockedOut = e.ClockedOut,
                ClockedIn = e.ClockedIn,
                ClockedInAt = e.ClockedInAt,
                ClockedOutAt = e.ClockedOutAt,
            }
        };
    }
}