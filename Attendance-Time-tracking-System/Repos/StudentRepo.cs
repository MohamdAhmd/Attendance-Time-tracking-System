using Microsoft.EntityFrameworkCore;

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
                student.User_Status = true;
                student.status = "Pending";
                student.Grade = 250;
                student.NextMinus = 0;
                student.roles.Add(new Roles { RoleId = 2 });
                student.AbsenceDays = 0;
                db.Students.Add(student);
                var done = db.SaveChanges();

              
                return done;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public Student GetStudentById(int id)
        {
            var model = db.Students.Where(x=>x.User_Status==true).Include(x=>x.TrackNavigation).FirstOrDefault(s => s.Id == id);
            return model;
        }

        public List<AttendanceList> GetAllUsersWithRole(int? value ,string daystatus)
        {

            var todaydate = DateTime.Now.Date;

            var exist = db.Days.Any(x => x.Day.Date == todaydate);
            var dayID = db.Days.FirstOrDefault(x => x.Day.Date == todaydate)?.Id;
      
            var students = db.Students.Include(x=>x.attends).
                Include(x=>x.TrackNavigation).
                ThenInclude(y=>y.trackDays).
                Where(x=>x.User_Status == true && exist &&x.status=="Accepted" 
                && x.TrackNavigation.trackDays.Any(td => td.DayId == dayID && td.Status == daystatus)).
                Select(x=> new AttendanceList
                {
                    f_name = x.F_name,
                    l_name = x.L_name,
                    id = x.Id,
                    attendpresent = x.attends.FirstOrDefault(y => y.DayId == dayID).Status,
                    attendleave = x.attends.FirstOrDefault(y => y.DayId == dayID).StatusOut
                }).ToList();


            return students;
        }


        public bool changeattendance(int userId, bool value)
        {
            try
            {
                var todaydate = DateTime.Now.Date;


                var dayID = db.Days.FirstOrDefault(x => x.Day.Date == todaydate).Id;
                var UserAttendance = db.Attends.FirstOrDefault(x => x.DayId == dayID && x.UserId == userId);
                var student = GetStudentById(userId);
                var usertrack = student.TrackId;
                var StartDateOfToday = db.TrackDays.Where(x=>x.DayId == dayID && x.TrackId== usertrack).FirstOrDefault()?.StartPeriod ??
                    DateTime.Today.Add(new TimeSpan(9, 0, 0));

                if (value && DateTime.Now > StartDateOfToday.AddMinutes(15)&&UserAttendance.Time == null)
                {
                    student.Grade -= student.NextMinus;
                    student.NextMinus += addminus(student.AbsenceDays.Value);
                    student.AbsenceDays += 1;
                }
                

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

        public int addminus(int numbr)
        {
            if (numbr % 3 == 0 && numbr / 3 <= 6)
                return 5;
            return 0;
        }

       
    }
}
