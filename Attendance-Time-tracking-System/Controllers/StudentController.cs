using Attendance_Time_tracking_System.Migrations;
using Attendance_Time_tracking_System.Models;
using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        readonly dbContext db;
       
        public StudentController(dbContext _db )
        {
            db= _db;
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
    }
}
