namespace Attendance_Time_tracking_System.Repos
{
    public class TrackRepo : ITrackRepo
    {
        readonly dbContext db;
        public TrackRepo(dbContext db)
        {
            this.db = db;
        }
    }
}
