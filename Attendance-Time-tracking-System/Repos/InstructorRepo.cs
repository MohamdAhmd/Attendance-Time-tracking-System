using Attendance_Time_tracking_System.Models;

namespace Attendance_Time_tracking_System.Repos
{
    public class InstructorRepo : IInstructorRepo
    {
        readonly dbContext db;
        public InstructorRepo(dbContext _db)
        {
            db = _db;
        }
    }
}
