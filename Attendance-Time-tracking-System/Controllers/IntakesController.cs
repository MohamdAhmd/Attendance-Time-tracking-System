using Microsoft.AspNetCore.Mvc;

namespace Attendance_Time_tracking_System.Controllers
{
    public class IntakesController : Controller
    {
        IIntakeRepo IntakeRepo;
        public IntakesController(IIntakeRepo _IntakeRepo)
        {
            IntakeRepo = _IntakeRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(IntakeRepo.GetAll());
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var model = IntakeRepo.GetById(id.Value);
            if (model == null)
                return NotFound();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public IActionResult Create(IntakeViewModel data)
        {
            if (!ModelState.IsValid)
                return View("Create", data);

            IntakeRepo.add(data);
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult Edit(Intake _intake)
        {
            if (ModelState.IsValid)
            {
                IntakeRepo.UpdateIntake(_intake);
                return RedirectToAction("Index");
            }
            return View(_intake);
        }

        public IActionResult Delete(int id)
        {
            IntakeRepo.DeleteIntake(id);
            return Content("Intake Deleted Successfully");
        }
    }
}
