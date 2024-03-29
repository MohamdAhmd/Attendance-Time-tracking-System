using Attendance_Time_tracking_System.Models;

namespace Attendance_Time_tracking_System.Repos
{
    public class AttendRepo : IAttendRepo
    {
        readonly dbContext db;
        public AttendRepo(dbContext _db)
        {
            db = _db;
        }
    }
}
