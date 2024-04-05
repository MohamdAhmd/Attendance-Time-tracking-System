using Microsoft.AspNetCore.Mvc;

namespace Attendance_Time_tracking_System.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepo employeeRepo;
        private dbContext db;

        public EmployeeController(IEmployeeRepo _employeeRepo, dbContext _db)
        {
            employeeRepo = _employeeRepo;
            db = _db;
        }
        public IActionResult Index()
        {
            var model = employeeRepo.GetAll();
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Role = db.Roles.ToList();
            return View();

        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {

            if (employeeRepo.IsEmailExist(emp.Email))
            {
                ViewBag.ErrorMsg = "This Email is already Exist ";
            }


            employeeRepo.Add(emp);

            return RedirectToAction("Index");

        }


        public IActionResult Delete(Employee emp)
        {
            var deleted = employeeRepo.GetById(emp.Id);
            employeeRepo.Delete(deleted);
            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var model = employeeRepo.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);

        }

        [HttpPost]
        public IActionResult Update(Employee emp, int id)
        {
            if (ModelState.IsValid)
            {
                emp.Id = id;
                employeeRepo.Update(emp);
                return RedirectToAction("Index");
            }

            return View(emp);
        }

    }
}
