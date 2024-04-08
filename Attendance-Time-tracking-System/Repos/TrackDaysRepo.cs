
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

        public void AddDay(TrackDays trackDays)
        {
            db.TrackDays.Add(trackDays);
            db.SaveChanges();
        }

        public void DeleteDay(int id)
        {
            var trackDays = db.TrackDays.FirstOrDefault(t => t.DayId == id);
            db.TrackDays.Remove(trackDays);
            db.SaveChanges();
        }

        public IEnumerable<TrackDays> GetAllDays()
        {
            return db.TrackDays.Include(t => t.DayNavigation).Include(t => t.TrackNavigation).ToList();
        }

        public TrackDays GetDayById(int id)
        {
            return db.TrackDays.Include(t => t.DayNavigation).Include(t => t.TrackNavigation).FirstOrDefault(t => t.DayId == id);
        }

        public void UpdateDay(TrackDays trackDays)
        { 
            db.TrackDays.Update(trackDays);
            db.SaveChanges();
        }
    }
}
