using Attendance_Time_tracking_System.IRepos;
using Attendance_Time_tracking_System.Migrations;
using Attendance_Time_tracking_System.Models;
using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;
using System.IO;
using Attendance_Time_tracking_System.IRepos;



namespace Attendance_Time_tracking_System.Controllers
{
    public class StudentController : Controller
    {
        readonly dbContext db;
        IStudentRepo studentRepo;
        IUserRepo userRepo;

        public StudentController(dbContext _db, IStudentRepo _studentRepo, IUserRepo _userRepo)
        {
            db = _db;
            studentRepo = _studentRepo;
            userRepo = _userRepo;
        }
        [Authorize(Roles = "Student")]
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
        [Authorize(Roles = "Student")]
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
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [Authorize(Roles = "Student-affairs")]

        public IActionResult Profile()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);
            var user = studentRepo.GetStudentById(id);
            return View(user);
        }


        public IActionResult ProfilePage()
        {
            var instid = stdid();
            var user = userRepo.GetUserEditById(instid);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEditProfile user, IFormFile personalimages)
        {
            if (ModelState.IsValid)
            {
                if (await userRepo.EditUserInfo(user, personalimages) == true)
                {
                    return RedirectToAction("ProfilePage");
                }
                return RedirectToAction("ProfilePage");
            }
            else
            {
                return RedirectToAction("ProfilePage");
            }
        }

        public IActionResult ChangePassword()
        {
            var instid = stdid();
            var userpass = userRepo.UserPassword(instid);
            return View(userpass);
        }
        [HttpPost]
        public IActionResult ChangePassword(UserPassword model)
        {
            if (ModelState.IsValid)
            {
                if (userRepo.updatePass(model))
                {
                    return RedirectToAction("ProfilePage");
                }
            }
            return View(model);
        }


        public int stdid()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim != null)
            {
                string userId = userIdClaim.Value;
                return int.Parse(userId);
            }
            return 7;
        }

    }
}
