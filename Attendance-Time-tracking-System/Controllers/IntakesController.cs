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

        public IActionResult Create(Intake data)
        {
            IntakeRepo.add(data);
            return Content("Intake Added Successfully");
        }
        [HttpPost]
        public IActionResult Edit(int id,[FromBody]Intake _intake)
        {
            IntakeRepo.UpdateIntake(_intake);
            return Content("Intake Updated Successfully");
        }

        public IActionResult Delete(int id)
        {
            IntakeRepo.DeleteIntake(id);
            return Content("Intake Deleted Successfully");
        }
    }
}
