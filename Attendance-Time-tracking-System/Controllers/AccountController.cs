using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.Controllers
{
    public class AccountController : Controller
    {
        IUserRepo userRepo;
        ITrackRepo trackRepo;
        IIntakeRepo intakeRepo;
        public AccountController(IUserRepo _userRepo , ITrackRepo _trackRepo , IIntakeRepo _IntakeRepo)
        {
            userRepo = _userRepo;
            trackRepo = _trackRepo;
            intakeRepo = _IntakeRepo;
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
    }
}
