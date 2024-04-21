using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Controllers
{
    public class InstructorController : Controller
    {
        IInstructorRepo instructorRepo;
        readonly ITrackRepo trackRepo;
        readonly IUserRepo userRepo;
        dbContext db;
        
        public InstructorController(IInstructorRepo _instructorRepo,dbContext _db, ITrackRepo trackRepo , IUserRepo userRepo)
        {
            instructorRepo = _instructorRepo;
            this.trackRepo = trackRepo;
            this.userRepo = userRepo;
            db = _db;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            
            return View(instructorRepo.GetAllInstructors());
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Instructor instructor)
        {
            instructorRepo.AddInstructor(instructor);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var instructor=db.Instructors.Include(i=>i.roles).Include(i=>i.supervisor).FirstOrDefault(i=>i.Id==id);
            var roles = db.RoleIds.ToList();
            var tracks = db.Tracks.ToList();
            ViewBag.Roles = roles;
            ViewBag.Tracks = tracks;
            return View(instructor);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Instructor instructor, int? id)
        {
            instructorRepo.UpdateInstructor(instructor,id);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            instructorRepo.DeleteInstructor(instructorRepo.GetInstructorById(id));
            return RedirectToAction("Index");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 

        [Authorize(Roles ="Supervisor")]
        public IActionResult SupervisorShowStudetnsDegrees()
        {
            int SupervisorId = instructorid();
            ViewBag.alltracks = trackRepo.GetAllTracksForSuperVisor(SupervisorId);
            ViewBag.choosendate =  DateTime.Now.ToString("yyyy-MM-dd");

            var students = new List<ShowStudentsSupervisor>();
            return View(students);
        }

        [Authorize(Roles = "Supervisor")]
        [HttpPost]
        public IActionResult SupervisorShowStudetnsDegrees(DateOnly choosendate , int SelectedTrack , string DayStatus)
        {

            int SupervisorId = instructorid();
            ViewBag.alltracks = trackRepo.GetAllTracksForSuperVisor(SupervisorId);
            ViewBag.choosendate = choosendate.ToString("yyyy-MM-dd");
            ViewBag.SelectedTrack = SelectedTrack;
            ViewBag.DayStatus = DayStatus;

            var getallstudent = instructorRepo.SuperVisorStudent(SupervisorId, choosendate, SelectedTrack, DayStatus);
            return View(getallstudent);
        }



        //show instructor absence status
        [Authorize(Roles = "Instructor")]
        public IActionResult showAttendance()
        {
            var instid = instructorid();
            var model = instructorRepo.ShowInstructorAbseneceDays(instid);
            return View(model);
        }
        [Authorize(Roles = "Instructor")]
        [HttpPost]
        public IActionResult showAttendance(string SelectedStatus)
        {
            var instid = instructorid();
            var model = instructorRepo.ShowInstructorAbseneceDays(instid, SelectedStatus);
            return View(model);
        }
        [Authorize(Roles = "Instructor")]
        [HttpPost]
        public IActionResult showAttendance2(string SelectedStatus)
        {
            var instid = instructorid();
            var model = instructorRepo.ShowInstructorAbseneceDays(instid, SelectedStatus);
            return PartialView(model);
        }

        //end of show instructor absent status
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        /// <summary>
        /// profile page
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Instructor")]
        public IActionResult ProfilePage()
        {
            var instid = instructorid();
            var user = userRepo.GetUserEditById(instid);
            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> EditProfile(UserEditProfile user, IFormFile personalimages)
        {
            if (ModelState.IsValid)
            {
                if (await userRepo.EditUserInfo(user, personalimages) == true)
                {
                    return RedirectToAction("ProfilePage");
                }
                return RedirectToAction("ProfilePage");
            }
            else
            {
                return RedirectToAction("ProfilePage");
            }
        }

        public IActionResult ChangePassword()
        {
            var instid = instructorid();
            var userpass = userRepo.UserPassword(instid);
            return View(userpass);
        }
        [HttpPost]
        public IActionResult ChangePassword(UserPassword model)
        {
            if (ModelState.IsValid)
            {
                if (userRepo.updatePass(model))
                {
                    return RedirectToAction("ProfilePage");
                }
            }
            return View(model);
        }


        public int instructorid()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim != null)
            {
                string userId = userIdClaim.Value;
                return int.Parse(userId);
            }
            return 7;
        }

        public IActionResult Profile()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);
            var user = instructorRepo.GetInstructorById(id);
            return View(user);
        }
    }
}
//
