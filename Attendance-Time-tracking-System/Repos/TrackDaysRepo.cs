
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
            return db.TrackDays.Include(t => t.DayNavigation).Include(t => t.TrackNavigation).FirstOrDefault(t => t.DayId == dayId && t.TrackId == trackId);
        }

        public IEnumerable<TrackDays> GetTrackDaysBySupervisorId(int id)
        {
            return db.TrackDays.Include(t => t.DayNavigation).Include(t => t.TrackNavigation).Where(t => t.TrackNavigation.SupervisorID == id).ToList();
        }

        public void UpdateTrackDay(TrackDays trackDays, int? dayId)
        {
            // Find the existing TrackDay entity
            var existingTrackDay = db.TrackDays.FirstOrDefault(t => t.DayId == trackDays.DayId && t.TrackId == trackDays.TrackId);

            if (existingTrackDay != null)
            {
                // Delete the existing TrackDay entity
                db.TrackDays.Remove(existingTrackDay);
                //get day navigation to pass it to the new track day
                var day = db.Days.FirstOrDefault(d => d.Id == dayId);
                
                // Create a new TrackDay entity with the updated association
                var newTrackDay = new TrackDays
                {
                    TrackId = trackDays.TrackId,
                    DayId = dayId.Value,
                    Status = trackDays.Status,
                    StartPeriod = day.Day,
                    Lecture1 = trackDays.Lecture1,
                    Lecture2 = trackDays.Lecture2,
                    Lecture3 = trackDays.Lecture3,
                    DayNavigation = day,
                    TrackNavigation = trackDays.TrackNavigation
                };

                // Add the new TrackDay entity to the context
                db.TrackDays.Add(newTrackDay);

                // Save changes to the database
                db.SaveChanges();
            }
        }

    }
}
