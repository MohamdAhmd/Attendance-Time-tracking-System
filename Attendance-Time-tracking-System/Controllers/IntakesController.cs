using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if (IntakeRepo.GetByName(data.IntakeName,data.ProgramName))
            {
                ModelState.AddModelError("IntakeName", "Intake name already exists.");
                return View("Create", data);
            }
            IntakeRepo.add(data);
            return RedirectToAction("index");

        
        }
        [HttpPost]
        public IActionResult Edit(Intake _intake)
        {
            if (!ModelState.IsValid)
            {
                return View(_intake);
            }
            IntakeRepo.UpdateIntake(_intake);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            IntakeRepo.DeleteIntake(id);
            return RedirectToAction("Index");
        }

    }
}
