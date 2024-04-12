using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Attendance_Time_tracking_System.Controllers
{
    public class InstructorController : Controller
    {
        IInstructorRepo instructorRepo;
        readonly ITrackRepo trackRepo;
        dbContext db;
        
        public InstructorController(IInstructorRepo _instructorRepo,dbContext _db, ITrackRepo trackRepo)
        {
            instructorRepo = _instructorRepo;
            this.trackRepo = trackRepo;
            db = _db;
        }
        public IActionResult Index()
        {
            
            return View(instructorRepo.GetAllInstructors());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            instructorRepo.AddInstructor(instructor);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var roles = db.RoleIds.ToList();
            var tracks = db.Tracks.ToList();
            ViewBag.Roles = roles;
            ViewBag.Tracks = tracks;
            return View(instructorRepo.GetInstructorById(id));
        }
        [HttpPost]
        public IActionResult Edit(Instructor instructor, int? id)
        {
            instructorRepo.UpdateInstructor(instructor,id);
            return RedirectToAction("Index");
        }
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
            ViewBag.alltracks= trackRepo.GetAllTracksForSuperVisor(SupervisorId);

            var students = new List<ShowStudentsSupervisor>();
            return View(students);
        }
        [Authorize(Roles = "Supervisor")]
        [HttpPost]
        public IActionResult SupervisorShowStudetnsDegrees(DateOnly choosendate , int SelectedTrack)
        {

            int SupervisorId = instructorid();
            ViewBag.alltracks = trackRepo.GetAllTracksForSuperVisor(SupervisorId);

            var getallstudent = instructorRepo.SuperVisorStudent(SupervisorId, choosendate, SelectedTrack);
            return View(getallstudent);
        }

        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "home");
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
    }
}
//
