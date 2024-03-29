namespace Attendance_Time_tracking_System.Repos
{
    public class StudentRepo :IStudentRepo
    {
        readonly dbContext db;
        public StudentRepo(dbContext db)
        {
            this.db = db;
        }
    }
}
