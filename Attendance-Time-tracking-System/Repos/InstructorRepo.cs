using Attendance_Time_tracking_System.Models;

namespace Attendance_Time_tracking_System.Repos
{
    public class InstructorRepo : IInstructorRepo
    {
        readonly dbContext db;
        public InstructorRepo(dbContext _db)
        {
            db = _db;
        }
        public List<Instructor> GetAllInstructors()
        {
            return db.Instructors.ToList();
        }
        public Instructor GetInstructorById(int id)
        {
            return db.Instructors.FirstOrDefault(i => i.Id == id);
        }
        public Instructor GetInstructorByUserId(int id)
        {
            return db.Instructors.FirstOrDefault(i => i.UserId == id);
        }
        public void AddInstructor(Instructor instructor)
        {
            db.Instructors.Add(instructor);
            db.SaveChanges();
        }
        public void UpdateInstructor(Instructor instructor)
        {
            db.Instructors.Update(instructor);
            db.SaveChanges();
        }
        public void DeleteInstructor(Instructor instructor)
        {
            db.Instructors.Remove(instructor);
            db.SaveChanges();
        }

    }
}
