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

        public Days GetTheDayId()
        {
            var model = db.Days.FirstOrDefault(x => x.Day.Date == DateTime.Today);
            return model;
        }
    }
}
