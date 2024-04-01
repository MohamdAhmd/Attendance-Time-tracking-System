using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.Controllers
{
    public class AccountController : Controller
    {
        IUserRepo userRepo;
        ITrackRepo trackRepo;
        IIntakeRepo intakeRepo;
        IStudentRepo studentRepo;
        public AccountController(IUserRepo _userRepo , ITrackRepo _trackRepo , IIntakeRepo _IntakeRepo , IStudentRepo _studentRepo)
        {
            userRepo = _userRepo;
            trackRepo = _trackRepo;
            intakeRepo = _IntakeRepo;
            studentRepo = _studentRepo;
        }
        public IActionResult login()
        {
            return View();
        }
        
        public IActionResult Register()
        {
            ViewBag.AllTracks = trackRepo.GetAllTracks();
            ViewBag.AllIntakes = intakeRepo.GetAllIntakes();
            return View();
        }
        [HttpPost]
        public IActionResult Register(Student student)
        {
            ViewBag.AllTracks = trackRepo.GetAllTracks();
            ViewBag.AllIntakes = intakeRepo.GetAllIntakes();

            try
            {
                if (studentRepo.AddStudent(student) > 0)
                {
                    return RedirectToAction("login");
                }
                else
                {
                    TempData["error"] = "database";
                    return RedirectToAction("Register");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }

            

        }
    }
}
