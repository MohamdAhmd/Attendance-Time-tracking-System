using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Time_tracking_System.Controllers
{
    public class LoginController : Controller
    {
        IUserRepo userRepo;
        public LoginController(IUserRepo _userRepo)
        {
            userRepo = _userRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
