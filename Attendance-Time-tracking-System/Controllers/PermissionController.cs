using Microsoft.AspNetCore.Mvc;

namespace Attendance_Time_tracking_System.Controllers
{
    public class PermissionController : Controller
    {
        IPermissionRepo PermissionRepo;
        public PermissionController(IPermissionRepo _PermissionRepo)
        {
            PermissionRepo = _PermissionRepo;
        }
        // CRUD ON Permission from student
        // 


        public IActionResult Index()
        {
            return View(PermissionRepo.GetPermissions());
        }
        [HttpGet]
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Permission _p)
        {
            if (ModelState.IsValid) // Check if model state is valid
            {
                PermissionRepo.create(_p);
                return RedirectToAction("Index");
            }
            // If model state is not valid, return the view with validation errors
            return View(_p);
        }

       public IActionResult Delete(string date)
        {
           PermissionRepo.delete(date);
           return RedirectToAction("Index");
        }

    }
}
