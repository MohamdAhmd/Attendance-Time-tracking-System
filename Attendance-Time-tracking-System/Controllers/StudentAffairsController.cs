using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Time_tracking_System.Controllers
{
    [Authorize(Roles = "Student-affairs")]
    public class StudentAffairsController : Controller
    {
        readonly IUserRepo userRepo;
        readonly IStudentRepo studentRepo;
        public StudentAffairsController(IUserRepo _userRepo , IStudentRepo _studentRepo)
        {
            userRepo = _userRepo;
            studentRepo = _studentRepo;
            this.studentRepo.PutAllStudentsInAttendanceTable("online");
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetUsers(int value)
        {
            try
            {
                if (value == 2)
                {
                    var users = studentRepo.GetAllUsersWithRole(value, "online");
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
        public IActionResult changeAttendstatus(int userId, bool value, int usertype)
        {
            try
            {
                if (usertype == 2)
                {
                    bool change = studentRepo.changeattendance(userId, value);
                    return change ? Ok(new { success = true }) : StatusCode(500, "Internal server error");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult changeLeavingstatus(int userId, bool value, int usertype)
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


        public IActionResult TakeAllLate(int[] value, int userType)
        {
            if (value.Length > 0)
            {
                return studentRepo.ChangeAllStudentToLate(value) ? Ok(new { success = true }) : StatusCode(500, "Internal server error.");
            }
            return StatusCode(500, "No items to save");
        }


        public IActionResult showlayout()
        {
            return View("_Layout");
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


        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "home");
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
