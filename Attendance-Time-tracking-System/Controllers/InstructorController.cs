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
            var roles = db.Roles.ToList();
            ViewBag.Roles = roles;
            return View(instructorRepo.GetInstructorById(id));
        }
        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            instructorRepo.UpdateInstructor(instructor);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            instructorRepo.DeleteInstructor(instructorRepo.GetInstructorById(id));
            return RedirectToAction("Index");
        }
    }
}
