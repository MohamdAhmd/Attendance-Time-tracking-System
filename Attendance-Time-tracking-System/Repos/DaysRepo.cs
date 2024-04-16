using Attendance_Time_tracking_System.Models;

namespace Attendance_Time_tracking_System.Repos
{
    public class DaysRepo : IDaysRepo
    {
        readonly dbContext db;
        public DaysRepo(dbContext _db)
        {
            db = _db; 
        }

        public void AddDay(Days day)
        {
            db.Days.Add(day);
            db.SaveChanges();
        }

        public void DeleteDay(int id)
        {
            var day = db.Days.FirstOrDefault(d => d.Id == id);
            db.Days.Remove(day);
            db.SaveChanges();
        }

        public List<Days> GetAllDays()
        {
            return db.Days.ToList();
        }

        public Days GetDayById(int id)
        {
            return db.Days.Find(id);
        }

        public void UpdateDay(Days day)
        {
            db.Days.Update(day);
            db.SaveChanges();
        }

        public Days GetTheDayId()
        {
            var model = db.Days.FirstOrDefault(x => x.Day.Date == DateTime.Today);
            return model;
        }

        public Days GetDayByDate(DateTime date)
        {
            return db.Days.FirstOrDefault(d => d.Day.Date == date.Date);
            
        }
    }
}
