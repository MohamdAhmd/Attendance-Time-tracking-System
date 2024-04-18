using Attendance_Time_tracking_System.Models;
using Attendance_Time_tracking_System.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Attendance_Time_tracking_System.Repos
{
    public class InstructorRepo : IInstructorRepo
    {
        readonly dbContext db;

        public InstructorRepo(dbContext _db)
        {
            db = _db;
        }

        public List<InstructorViewModel> GetAllInstructors()
        {
            var Instructors = (from user in db.Users
                               join instructor in db.Instructors on user.Id equals instructor.Id
                               select new InstructorViewModel
                               {
                                   Id = instructor.Id,
                                   Name = $"{user.F_name} {user.L_name}",
                                   Email = user.Email,
                                   Password = user.Password,
                                   Salary = instructor.Salary,
                                   HireDate = instructor.HireDate,



                               }).ToList();



            return Instructors;
        }
        public Instructor GetInstructorById(int id)
        {
            return db.Instructors.Include(x => x.roles).FirstOrDefault(i => i.Id == id);
        }

        public void AddInstructor(Instructor instructor)
        {
            instructor.roles.Add(new Roles { RoleId = 3 });
            db.Instructors.Add(instructor);
            db.SaveChanges();
        }
        public void UpdateInstructor(Instructor instructor, int? id)
        {
            // var track=db

            //instructor.supervisor= db.Instructors.FirstOrDefault(i => i.Id == id);

            db.Instructors.Update(instructor);

            db.SaveChanges();
        }
        public void DeleteInstructor(Instructor instructor)
        {
            db.Instructors.Remove(instructor);
            db.SaveChanges();
        }


        public List<ShowStudentsSupervisor> SuperVisorStudent(int supervisorid, DateOnly day, int SelectedTrack, string? DayStatus)
        {
            DateTime daydate = new DateTime(day.Year, day.Month, day.Day);
            var choosendate = daydate.Date;

            var trackid = db.Tracks.FirstOrDefault(x => x.SupervisorID == supervisorid && x.Id == SelectedTrack)?.Id;
            var dayexist = db.Days.FirstOrDefault(x => x.Day.Date == choosendate)?.Id;
            var dayexistintrack = db.TrackDays.Any(x => x.TrackId == trackid && x.DayId == dayexist);


            if (trackid != null && dayexist != null && dayexistintrack)
            {
                var allstudents = db.Students.Include(x => x.attends).Include(x => x.TrackNavigation)
                                    .Where(x => x.TrackId == trackid
                                    //&& x.attends.Any(x => x.attendstatus == DayStatus && x.DayId == dayexist)
                                    && (DayStatus == null || x.attends.Any(x => x.attendstatus == DayStatus && x.DayId == dayexist)
                                    || (DayStatus == "Absent" && !x.attends.Any(x => x.DayId == dayexist))
                                    ))
                                    .Select(x => new ShowStudentsSupervisor
                                    {
                                        Fullname = x.F_name + " "+x.L_name,
                                        attendance = x.attends.FirstOrDefault(y=>y.DayId==dayexist && y.UserId==x.Id).attendstatus ?? "Absent",
                                        
                                        grade = x.attends.FirstOrDefault(y => y.DayId == dayexist && y.UserId == x.Id).StudentDegreeAtMoment ??
                                        x.attends.OrderByDescending(y=>y.StudentDegreeAtMoment).LastOrDefault(y=>y.DayId<=dayexist && y.UserId==x.Id).StudentDegreeAtMoment ?? 250,//250,
                                        trackname = x.TrackNavigation.Name
                                    }).ToList();

                return allstudents;
            }

            List<ShowStudentsSupervisor> one = new List<ShowStudentsSupervisor>();
            return one;
        }
    
    
        public List<UserAbsence> ShowInstructorAbseneceDays(int id , string userstatus="Absent")
        {
            var model = db.Attends.Include(x => x.DaysNavigation).Where(x => x.UserId == id)
                            .Where(x => x.attendstatus==userstatus)
                            .Select(x => new UserAbsence
                            {
                                day = DateOnly.FromDateTime(x.DaysNavigation.Day),
                                status = userstatus

                            }).ToList();
            return model;
        }
    }
}
