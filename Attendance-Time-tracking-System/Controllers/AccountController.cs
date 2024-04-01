using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
        [HttpPost]
        public async Task<IActionResult> login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = userRepo.GetUser(loginVM.Email, loginVM.Password);
            if (user == null)
            {
                TempData["NOT Found"] = "No Such user exist please enter a valid inputs";
                return View(loginVM);
            }
            Claim claim1 = new Claim(ClaimTypes.Name, user.F_name + " " + user.L_name);
            Claim claim2 = new Claim(ClaimTypes.Email, user.Email);
            List<Claim> claims = new List<Claim>();

            List<string> loginroles = new List<string>();

            foreach(var role in user.roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role.Role));
                loginroles.Add(role.Role);
            }
            ClaimsIdentity claimsIdentity1 = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            claimsIdentity1.AddClaim(claim1);
            claimsIdentity1.AddClaim(claim2);
            claimsIdentity1.AddClaims(claims);

            ClaimsPrincipal claimsPrincipal1 = new ClaimsPrincipal();
            claimsPrincipal1.AddIdentity(claimsIdentity1);

            await HttpContext.SignInAsync(claimsPrincipal1);

            if (loginroles.Contains("Admin"))
            {
                return RedirectToAction("Admin","Account");
            }
            else if (loginroles.Contains("Instructor"))
            {
                return RedirectToAction("Instructor", "Account");
            }
            else if(loginroles.Contains("Student"))
            {
                return RedirectToAction("Student", "Account");
            }
            else
            {
                return RedirectToAction("Employee", "Account");
            }
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

        public IActionResult showusers()
        {
            List<User> users = userRepo.GetAllUsers();
            return Content(users[0].Id.ToString());
        }
    }
}
