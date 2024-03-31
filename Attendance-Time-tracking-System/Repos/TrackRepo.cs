using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Repos
{
    public class TrackRepo : ITrackRepo
    {
        readonly dbContext db;
        public TrackRepo(dbContext db)
        {
            this.db = db;
        }

        public List<Track> GetAllTracks()
        {
            var model = db.Tracks.Include(x=>x.InstructorNavigation).Include(x=>x.ProgramNavigation).ToList();
            return model;
        }
    }
}
