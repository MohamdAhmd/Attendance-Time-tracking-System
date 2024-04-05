using Attendance_Time_tracking_System.Models;

namespace Attendance_Time_tracking_System.Repos
{
    public class IntakeRepo : IIntakeRepo
    {
        readonly dbContext db;
        public IntakeRepo(dbContext db)
        {
            this.db = db;
        }

        public List<Intake> GetAllIntakes()
        {
            var model = db.Intakes.ToList();
            return model;
        }
    }
}
