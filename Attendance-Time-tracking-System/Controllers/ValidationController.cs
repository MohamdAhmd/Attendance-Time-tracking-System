using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Attendance_Time_tracking_System.Controllers
{
    public class ValidationController : Controller
    {
        public IActionResult confirmpassword(string ConfirmPassword ,string Password )
        {
            bool valid = Password == ConfirmPassword;
            return Json(valid);
        }
    }
}
