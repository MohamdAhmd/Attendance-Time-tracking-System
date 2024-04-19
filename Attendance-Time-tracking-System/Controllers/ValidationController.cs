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


        public IActionResult IsPassword (string oldPassword, int Id)
        {
            return Json(userRepo.currentpass(oldPassword, Id)); 
        }

        public IActionResult IsEqualToPassword (string oldPassword, string newPassword)
        {
            bool valid = oldPassword == newPassword; 
            return Json(!valid);
        }
        public IActionResult ConfirmPasswords(string ConfirmPassword, string NewPassword)
        {
            bool valid = ConfirmPassword == NewPassword;
            return Json(valid);
        }
    }
}
