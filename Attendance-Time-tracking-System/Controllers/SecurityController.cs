using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.CodeDom;
using System.Reflection.Metadata.Ecma335;

namespace Attendance_Time_tracking_System.Controllers
{
    [Authorize(Roles = "Security")]
    public class SecurityController : Controller
    {
        readonly IUserRepo userRepo;
        readonly IStudentRepo studentRepo;
        public SecurityController(IUserRepo userRepo, IStudentRepo studentRepo)
        {

            this.userRepo = userRepo;
            this.studentRepo = studentRepo;
            this.userRepo.PutAllUsersInAttendanceTable();
            this.studentRepo.PutAllStudentsInAttendanceTable("offline");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetUsers(int value)
        {
            try
            {
                
                if (value != 2)
                {
                    var users = userRepo.GetAllUsersWithRole(value);
                    return Json(users);
                }
                else if(value==2)
                {
                    var users = studentRepo.GetAllUsersWithRole(value, "offline");
                    return Json(users);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        public IActionResult changeAttendstatus(int userId , bool value , int usertype)
        {
            try
            {
                if (usertype == 5 || usertype == 6 || usertype==3)
                {
                    bool change = userRepo.changeattendance(userId, value , usertype);
                    return change ? Ok(new { success = true }) : StatusCode(500, "Internal server error.");
                }
                else if(usertype == 2)
                {
                    bool change = studentRepo.changeattendance(userId,value);
                    return change ? Ok(new { success = true }) : StatusCode(500, "Internal server error");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpPost]
        public IActionResult changeLeavingstatus (int userId , bool value , int usertype)
        {
            try
            {
                bool change = userRepo.ChangeLeaveAttendance(userId, value);
                return change ? Ok(new { success = true }) : StatusCode(500, "Internal server error.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        public IActionResult TakeAllLate (int[] value ,  int userType)
        {
            if (userType != 2 && value.Length>0)
            {
               return  userRepo.ChangeAllToLate(value) ? Ok(new { success = true }) : StatusCode(500, "Internal server error.");
            }
            else if(value.Length>0) 
            {
                return studentRepo.ChangeAllStudentToLate(value) ? Ok(new { success = true }) : StatusCode(500, "Internal server error.");
            }
            return StatusCode(500,"No items to save");
        }
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index","home");
        }

        /// <summary>
        /// profile page
        /// </summary>
        /// <returns></returns>

        public IActionResult ProfilePage()
        {
            var instid = instructorid();
            var user = userRepo.GetUserById(instid);
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
            var instid = instructorid();
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


        public int instructorid()
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
