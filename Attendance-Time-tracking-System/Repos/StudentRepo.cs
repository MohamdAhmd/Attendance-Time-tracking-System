using Attendance_Time_tracking_System.Migrations;
using Attendance_Time_tracking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Repos
{
    public class StudentRepo :IStudentRepo
    {
        readonly dbContext db;
        readonly IBlobRepo blobRepo;
        public StudentRepo(dbContext db, IBlobRepo blobRepo)
        {

            this.db = db;
            this.blobRepo = blobRepo;
        }

        public async Task<int> AddStudent(Student student,IFormFile personalimage)
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
                student.image = await blobRepo.AddingImage(personalimage);
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
                    attendpresent = x.attends.FirstOrDefault(y => y.DayId == dayID && x.Id==y.UserId).Status,
                    attendleave = x.attends.FirstOrDefault(y => y.DayId == dayID && x.Id == y.UserId).StatusOut,
                    status = x.attends.FirstOrDefault(y=>y.DayId == dayID && x.Id == y.UserId).attendstatus
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

                if (value && DateTime.Now > StartDateOfToday.AddMinutes(15))
                {
                    student.Grade -= havePermission(userId)? student.NextMinus/2 : student.NextMinus;
                    student.NextMinus += addminus(student.AbsenceDays.Value);
                    student.AbsenceDays += 1;
                }
                

                if (UserAttendance != null)
                {

                    UserAttendance.Status = value;
                    UserAttendance.Time = DateTime.Now;
                    if (value == false) { UserAttendance.StatusOut = false; }
                    if(DateTime.Now > StartDateOfToday.AddMinutes(15)){  UserAttendance.attendstatus = "Late";}
                    else if(DateTime.Now <= StartDateOfToday.AddMinutes(15)) { UserAttendance.attendstatus = "OnTime"; }
                    UserAttendance.StudentDegreeAtMoment = student.Grade;
                }
                else
                {
                    var userattend = new Attend
                    {
                        UserId = userId,
                        DayId = dayID,
                        Time = DateTime.Now,
                        Status = value,
                        StatusOut = false,
                        StudentDegreeAtMoment = student.Grade
                    };
                    if (DateTime.Now > StartDateOfToday.AddMinutes(15)) { userattend.attendstatus = "Late"; }
                    else if (DateTime.Now <= StartDateOfToday.AddMinutes(15)) { userattend.attendstatus = "OnTime"; }
                    db.Attends.Add(userattend);
                }

                if(value == false)
                {
                    UserAttendance.attendstatus = null;
                    UserAttendance.Time = null;
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

        public bool havePermission(int id)
        {
            var todaysdate = DateTime.Now.Date;
            var permission = db.Permissions.FirstOrDefault(x => x.StudentId == id && x.PermissionStatus == "Accepted" && x.day == todaysdate);
            if (permission == null)
            {
                return false;
            }
            return true;
        }
       

        public bool PutAllStudentsInAttendanceTable(string daystatus )
        {
            var todaydate = DateTime.Now.Date;
            var dayID = db.Days.FirstOrDefault(x => x.Day.Date == todaydate)?.Id;

            var students = db.Students.Include(x => x.attends).Include(x => x.roles).Include(x => x.TrackNavigation).ThenInclude(y => y.trackDays)
                            .Where(x => x.roles != null && x.roles.Any() && x.User_Status && x.status == "Accepted")
                            .Where(x => x.TrackNavigation.trackDays.Any(td => td.DayId == dayID && td.TrackId == x.TrackId && td.Status == daystatus))
                            .ToList();

            if (dayID != null)
            {
                var attendsToAdd = students
                    .Where(x => !x.attends.Any(y => y.DayId == dayID && y.UserId == x.Id))
                    .Select(user => new Attend { UserId = user.Id, DayId = dayID.Value, Status = false, StatusOut = false }).ToList();

                db.Attends.AddRange(attendsToAdd);
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else { return false; }
            }

                return false;
        }
    
        public bool ChangeAllStudentToLate(int[] ids)
        {
            var todaydate = DateTime.Now.Date;
            var dayID = db.Days.FirstOrDefault(x => x.Day.Date == todaydate)?.Id;
            if(dayID != null)
            {
                var attendance = db.Attends.Where(x => x.attendstatus == null && x.DayId == dayID).ToList();
                foreach (var item in ids)
                {
                    var attend = attendance.FirstOrDefault(x => x.UserId == item);
                        attend.attendstatus = "Absent";
                    
                    var student = db.Students.FirstOrDefault(x => x.Id == item);
                    if (student != null)
                    {
                        student.Grade -= havePermission(item) ? student.NextMinus / 2 : student.NextMinus;
                        student.NextMinus += addminus(student.AbsenceDays.Value);
                        student.AbsenceDays += 1;
                    }
                    attend.StudentDegreeAtMoment = student.Grade;
                }
                if (db.SaveChanges() > 0) { return true; }
                else { return false; }
            }
            return false;
        }

        public List<Student> GetAllPendingStudents()
        {
            return db.Students.Where(std => std.status == "Pending").ToList();
        }

        public void ChangeStatus(int stdId, string status)
        {
            var student = GetStudentById(stdId);
            student.status = status;
            if (status == "Rejected")
            {
                var std = db.Users.FirstOrDefault(a => a.Id == stdId);
                db.Remove(std);
                db.SaveChanges();
            }
            db.SaveChanges();
        }

        public void AddBulkStudents(List<Student> students)
        {
           
        }
    }
}
