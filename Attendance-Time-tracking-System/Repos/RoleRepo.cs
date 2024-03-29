namespace Attendance_Time_tracking_System.Repos
{
    public class RoleRepo : IRoleRepo
    {
        readonly dbContext db;
        public RoleRepo(dbContext db)
        {
            this.db = db;
        }
    }
}
