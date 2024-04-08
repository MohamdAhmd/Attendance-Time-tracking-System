using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.CodeDom;

namespace Attendance_Time_tracking_System.Controllers
{
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

        public IActionResult SetCookie()
        {
            try
            {
                // Set the cookie with the name "id" and value "cookie"
                Response.Cookies.Append("id", "cookie", new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(10)
                });

                return Ok("Cookie set successfully.");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
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

        public IActionResult datatable()
        {
            return View();
        }
    

    }
}
