namespace Attendance_Time_tracking_System.Repos
{
    public class StudentRepo :IStudentRepo
    {
        readonly dbContext db;
        public StudentRepo(dbContext db)
        {
            this.db = db;
        }

        public int AddStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    throw new ArgumentNullException();
                }
                int saved = 0;
                student.Status = true;
                student.status = "Pending";
                student.Grade = 250;
                student.NextMinus = 0;
                db.Students.Add(student);
                var done = db.SaveChanges();
                if (done > 0)
                {
                    db.Roles.Add(new Roles { UserId = student.Id, Role = "Student" });
                    saved = db.SaveChanges();
                }
                return saved;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public Student GetStudentById(int id)
        {
            var model = db.Students.FirstOrDefault(s => s.Id == id);
            return model;
        }
    }
}
