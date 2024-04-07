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
                //int saved = 0;
                student.User_Status = true;
                student.status = "Pending";
                student.Grade = 250;
                student.NextMinus = 0;
                student.roles.Add(new Roles { RoleId = 2 });

                db.Students.Add(student);
                var done = db.SaveChanges();

                //if (done > 0)
                //{
                //    db.Roles.Add(new Roles { UserId = student.Id, RoleId = 2 });
                //    saved = db.SaveChanges();
                //}
                return done;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public Student GetStudentById(int id)
        {
            var model = db.Students.Where(x=>x.User_Status==true).FirstOrDefault(s => s.Id == id);
            return model;
        }

        public bool changeattendance(int userId, bool value)
        {
            try
            {
                var dayID = db.Days.FirstOrDefault(x => x.Day.Date == DateTime.Today).Id;
                var UserAttendance = db.Attends.FirstOrDefault(x => x.DayId == dayID && x.UserId == userId);
                var NumberOfAbsenceDays = db.Attends.Where(x => x.UserId == userId && x.DayId != dayID && x.Status==false).Count();
                if (UserAttendance != null)
                {

                    UserAttendance.Status = value;
                    UserAttendance.Time = DateTime.Now;
                    if (value == false) { UserAttendance.StatusOut = false; }
                }
                else
                {
                    var userattend = new Attend
                    {
                        UserId = userId,
                        DayId = dayID,
                        Time = DateTime.Now,
                        Status = value,
                        StatusOut = false
                    };
                    db.Attends.Add(userattend);
                }

                if (value)
                {
                    
                }

                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
