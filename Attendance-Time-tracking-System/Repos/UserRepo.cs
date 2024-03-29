namespace Attendance_Time_tracking_System.Repos
{
    public class UserRepo  : IUserRepo
    {
        readonly dbContext db;
        public UserRepo(dbContext db)
        {
            this.db = db;
        }
    }
}
