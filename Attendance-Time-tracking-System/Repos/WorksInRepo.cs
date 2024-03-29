namespace Attendance_Time_tracking_System.Repos
{
    public class WorksInRepo: IWorksInRepo
    {
        readonly dbContext db;
        public WorksInRepo(dbContext db)
        {
            this.db = db;
        }
    }
}
