using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Controllers
{
   [Authorize(Roles = "Admin")]
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
            ViewBag.Role = db.RoleIds.Where(a => a.Id == 5 || a.Id == 6).ToList();
            return View();

        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            var x = Request.Form["roles"];
            emp.roles = new List<Roles> { new Roles() { RoleId = int.Parse(x) } };

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

            ViewBag.Role = db.RoleIds.Where(a => a.Id == 5 || a.Id == 6).ToList();

            if (model == null)
            {
                return NotFound();
            }
            return View(model);

        }

        [HttpPost]
        public IActionResult Update(Employee emp, int id)
        {

            emp.Id = id;
            var x = Request.Form["roles"];
            emp.roles = new List<Roles> { new Roles() { RoleId = int.Parse(x) } };
            employeeRepo.Update(emp);
            return RedirectToAction("Index");

        }

    }
}
