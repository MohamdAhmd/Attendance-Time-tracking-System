using Attendance_Time_tracking_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Attendance_Time_tracking_System.Repos
{
    public class UserRepo  : IUserRepo
    {
        readonly dbContext db;
        public UserRepo(dbContext db)
        {
            this.db = db;
        }

        public List<User> GetAllUsers()
        {
            var model = db.Users.ToList();
            return model;
        }

        public User GetUser(string Email, string Password)
        {
            var user = db.Users.Where(x => x.User_Status == true).Include(x=>x.roles).ThenInclude(x=>x.RoleNavigation).FirstOrDefault(x=>x.Email == Email && x.Password == Password);
            return user;
        }

        public bool isunique(string Email , int Id)
        {
            if (Id == 0)
            {
                return !db.Users.Any(x => x.Email == Email);
            }
            else
            {
                return !db.Users.Any(x => x.Email == Email && x.Id != Id);
            }
        }
    
        public List<AttendanceList> GetAllUsersWithRole(int value)
        {
            try
            {

                var todaydate = DateTime.Now.Date;

                var exist = db.Days.Any(x => x.Day.Date == todaydate);
                var dayyy = db.Days.Where(x => x.Day.Date == todaydate).ToList();
                var dayID = db.Days.FirstOrDefault(x => x.Day.Date == todaydate)?.Id ?? 0;

                var users = db.Users.Include(x => x.roles).Include(x => x.attends)
                            .Where(x => x.roles.Any(y => y.RoleId == value) && x.User_Status == true && exist)
                            .Select(x => new AttendanceList
                            {
                                f_name = x.F_name,
                                l_name = x.L_name,
                                id = x.Id,
                                attendpresent = x.attends.FirstOrDefault(y => y.DayId == dayID && x.Id == y.UserId).Status,
                                attendleave = x.attends.FirstOrDefault(y => y.DayId == dayID && x.Id == y.UserId).StatusOut,
                                status = x.attends.FirstOrDefault(y => y.DayId == dayID && x.Id == y.UserId).attendstatus
                            }).ToList();
                return users;
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public bool changeattendance (int userId , bool value ,int usertype)
        {
            try
            {
                var todaydate = DateTime.Now.Date;
                DateTime todayat915 = Latedate(usertype);


                var dayID = db.Days.FirstOrDefault(x => x.Day.Date == todaydate).Id;
                var UserAttendance = db.Attends.FirstOrDefault(x => x.DayId == dayID && x.UserId == userId);
                if (UserAttendance != null)
                {
                    
                    UserAttendance.Status = value;
                    UserAttendance.Time = DateTime.Now;
                    if (value==false) { UserAttendance.StatusOut = false; }
                    if(DateTime.Now > todayat915) { UserAttendance.attendstatus = "Late"; }
                    else if(DateTime.Now <= todayat915){ UserAttendance.attendstatus = "OnTime"; }
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
                        
                    };
                    if (DateTime.Now > todayat915) { userattend.attendstatus = "Late"; }
                    else if (DateTime.Now <= todayat915) { userattend.attendstatus = "OnTime"; }
                    db.Attends.Add(userattend);
                }
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public bool ChangeLeaveAttendance(int userId , bool value)
        {
            try
            {
                var todaydate = DateTime.Now.Date;

                var dayID = db.Days.FirstOrDefault(x => x.Day.Date == todaydate).Id;
                var UserAttendance = db.Attends.FirstOrDefault(x => x.DayId == dayID && x.UserId == userId);
                if (UserAttendance != null)
                {
                    UserAttendance.StatusOut = value;
                }
                else
                {
                    var userattend = new Attend
                    {
                        UserId = userId,
                        DayId = dayID,
                        Time = DateTime.Now,
                        Status = true,
                        StatusOut = value
                    };
                    db.Attends.Add(userattend);
                }
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    
        public DateTime Latedate(int usertype)
        {
            DateTime todaydate;
            if(usertype != 3)
            {
                todaydate = DateTime.Today.AddHours(8);
            }
            else
            {
                todaydate = DateTime.Today.AddHours(9).AddMinutes(15); 
            }
            return todaydate;
        }
        
        public bool PutAllUsersInAttendanceTable()
        {
            var users = db.Users.Include(x => x.roles).Include(x=>x.attends)
                .Where(x=> x.roles != null && x.roles.Any() && x.User_Status)
                .Where(x => !x.roles.Any(y => y.RoleId == 2 || y.RoleId == 1 )  ).ToList();

            var todaydate = DateTime.Now.Date;
            var dayID = db.Days.FirstOrDefault(x => x.Day.Date == todaydate)?.Id;
            if (dayID != null) {
                var attendsToAdd = users
                    .Where(x=>!x.attends.Any(y=>y.DayId==dayID &&y.UserId==x.Id))
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
    
    }
}
