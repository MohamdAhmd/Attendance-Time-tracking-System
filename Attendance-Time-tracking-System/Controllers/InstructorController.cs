using Microsoft.AspNetCore.Mvc;

namespace Attendance_Time_tracking_System.Controllers
{
    public class InstructorController : Controller
    {
        IInstructorRepo instructorRepo;
        dbContext db;
        
        public InstructorController(IInstructorRepo _instructorRepo,dbContext _db)
        {
            instructorRepo = _instructorRepo;
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
    }
}
