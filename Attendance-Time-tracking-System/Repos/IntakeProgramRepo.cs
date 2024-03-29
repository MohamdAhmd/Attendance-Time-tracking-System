using Attendance_Time_tracking_System.Models;

namespace Attendance_Time_tracking_System.Repos
{
    public class IntakeProgramRepo : IIntakeProgramRepo
    {
        readonly dbContext db;
        public IntakeProgramRepo(dbContext db)
        {
            this.db = db;
        }
    }
}
