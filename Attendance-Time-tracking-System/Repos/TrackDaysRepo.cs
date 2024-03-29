namespace Attendance_Time_tracking_System.Repos
{
    public class TrackDaysRepo : ITrackDaysRepo
    {
        readonly dbContext db;
        public TrackDaysRepo(dbContext db)
        {
            this.db = db;
        }
    }
}
