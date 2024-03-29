using Attendance_Time_tracking_System.Models;

namespace Attendance_Time_tracking_System.Repos
{
    public class DaysRepo : IDaysRepo
    {
        readonly dbContext db;
        public DaysRepo(dbContext _db)
        {
            db = _db; 
        }
    }
}
