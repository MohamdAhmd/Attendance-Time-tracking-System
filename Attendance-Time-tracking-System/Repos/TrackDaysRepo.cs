
using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Repos
{
    public class TrackDaysRepo : ITrackDaysRepo
    {
        readonly dbContext db;
        public TrackDaysRepo(dbContext db)
        {
            this.db = db;
        }

        public void AddTrackDay(TrackDays trackDays)
        {
            db.TrackDays.Add(trackDays);
            db.SaveChanges();
        }

        public void DeleteTrackDay(int id)
        {
            var trackDays = db.TrackDays.FirstOrDefault(t => t.DayId == id);
            db.TrackDays.Remove(trackDays);
            db.SaveChanges();
        }

        public IEnumerable<TrackDays> GetAllTrackDays()
        {
            return db.TrackDays.Include(t => t.DayNavigation).Include(t => t.TrackNavigation).ToList();
        }

        public TrackDays GetTrackDayById(int dayId, int trackId)
        {
            //return db.TrackDays.Include(t => t.DayNavigation).Include(t => t.TrackNavigation).FirstOrDefault(t => t.DayId == id);

            return db.TrackDays.Include(t => t.DayNavigation).Include(t => t.TrackNavigation).FirstOrDefault(t => t.DayId == dayId && t.TrackId == trackId);
        }

        public void UpdateTrackDay(TrackDays trackDays)
        { 
            db.TrackDays.Update(trackDays);
            db.SaveChanges();
        }
    }
}
