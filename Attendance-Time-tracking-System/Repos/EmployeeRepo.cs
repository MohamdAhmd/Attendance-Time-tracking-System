using Attendance_Time_tracking_System.Models;

namespace Attendance_Time_tracking_System.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        readonly dbContext db;
        public EmployeeRepo(dbContext _db)
        {
            db = _db;
        }
    }
}
