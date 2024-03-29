namespace Attendance_Time_tracking_System.Repos
{
    public class ProgramRepo : IProgramRepo
    {
        readonly dbContext db;
        public ProgramRepo(dbContext db)
        {
            this.db = db;
        }
    }
}
