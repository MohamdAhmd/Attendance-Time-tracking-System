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
            return db.Tracks.Include(t => t.InstructorNavigation).Include(t => t.ProgramNavigation).ToList();
        }
        public Track GetTrackById(int id)
        {
            return db.Tracks.Include(t => t.InstructorNavigation).Include(t => t.ProgramNavigation).FirstOrDefault(t => t.Id == id);
        }
        public void AddTrack(Track track)
        {
            db.Tracks.Add(track);

            db.SaveChanges();

        }
        public void UpdateTrack(Track track)
        {
            db.Tracks.Update(track);
            db.SaveChanges();
        }
        public void DeleteTrack(int id)
        {
            var track = db.Tracks.FirstOrDefault(t => t.Id == id);
            db.Tracks.Remove(track);
            db.SaveChanges();
        }

        public List<Instructor> GetAllInstructors()
        {
            return db.Instructors.ToList();
        }
    }
}
