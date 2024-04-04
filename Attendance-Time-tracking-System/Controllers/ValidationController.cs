using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Diagnostics.CodeAnalysis;

namespace Attendance_Time_tracking_System.Controllers
{
    public class ValidationController : Controller
    {
        IUserRepo userRepo;
        public ValidationController(IUserRepo _userRepo)
        {

            userRepo = _userRepo;

        }
        public IActionResult confirmpassword(string ConfirmPassword ,string Password )
        {
            bool valid = Password == ConfirmPassword;
            return Json(valid);
        }
        public IActionResult GraduationYear(DateTime GraduationYear)
        {
            return Json(DateTime.Now > GraduationYear);
        }

        public IActionResult uniqueEmail(string Email , int Id) 
        {
            return Json(userRepo.isunique(Email,Id));
        }

    }
}
