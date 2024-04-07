using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.CodeDom;

namespace Attendance_Time_tracking_System.Controllers
{
    public class SecurityController : Controller
    {
        readonly IUserRepo userRepo;
        public SecurityController(IUserRepo userRepo)
        {

            this.userRepo = userRepo;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetUsers(int value)
        {
           
            var users = userRepo.GetAllUsersWithRole(value);
                
            return Json(users);
            
        }
        [HttpPost]
        public IActionResult changeAttendstatus(int userId , bool value , int usertype)
        {
            try
            {
                if (usertype == 5 || usertype == 6 || usertype==3)
                {
                    bool change = userRepo.changeattendance(userId, value);
                    return change ? Ok(new { success = true }) : StatusCode(500, "Internal server error.");
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

    }
}
