using Attendance_Time_tracking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Repos
{
    public class IntakeRepo : IIntakeRepo
    {
        readonly dbContext db;
        public IntakeRepo(dbContext db)
        {
            this.db = db;
        }
        public List<Intake> GetAll()
        {
            return db.Intakes.ToList();
        }
            
        public void add(Intake _intake)
        {
            db.Intakes.Add(_intake);
            db.SaveChanges();
        }
        public void UpdateIntake(Intake _Data)
        {
            var model = db.Intakes.FirstOrDefault(a => a.Id == _Data.Id);
            model.Name = _Data.Name;
            db.SaveChanges();

        }
        public void DeleteIntake(int id)
        {
            Intake _intake = db.Intakes.FirstOrDefault(a => a.Id == id);
            db.Intakes.Remove(_intake);
            db.SaveChanges();

        }
    }
}
