using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Time_tracking_System.Controllers
{
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


        public IActionResult StudentRequests()
        {
            var data = studentRepo.GetAllPendingStudents();
            return View(data);
        }
        public IActionResult ChangeStatus(int stdId, string status,string dummy)
        {
            studentRepo.ChangeStatus(stdId, status);
            return RedirectToAction("StudentRequests");
        }

    }
}
