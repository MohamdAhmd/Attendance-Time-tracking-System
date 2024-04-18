using Attendance_Time_tracking_System.IRepos;
using Attendance_Time_tracking_System.Migrations;
using Attendance_Time_tracking_System.Models;
using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        readonly dbContext db;
        IStudentRepo studentRepo;

        public StudentController(dbContext _db, IStudentRepo _studentRepo)
        {
            db= _db;
            studentRepo = _studentRepo;

        }
        public IActionResult ShowAttendance()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);

            var model = db.Attends.Where(a=>a.UserId==id )       
                        .Include(a => a.UserNavigation)
                        .Include(a => a.DaysNavigation)  
                        .ToList();
                        ;
            return View(model);       

        }
        public IActionResult StudentSchedule()
        {

            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);
            //get the student schedule by his id and the track id then oder it by the date 
            var model = db.TrackDays.Where(t => t.TrackNavigation.Students.Any(s => s.Id == id))
                        .Include(t => t.DayNavigation)
                        .Include(t => t.TrackNavigation)
                        .ToList().OrderBy(t => t.DayNavigation.Day.Date);
            return View(model);
        }
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        public IActionResult Profile()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);
            var user = studentRepo.GetStudentById(id);
            return View(user);
        }

    }
}
