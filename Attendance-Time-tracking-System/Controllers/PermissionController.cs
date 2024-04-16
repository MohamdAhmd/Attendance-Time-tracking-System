using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Student")]
        public IActionResult Index()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);
            return View(PermissionRepo.GetPermissions(id));
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public IActionResult Create(Permission _p)
        {
            if (ModelState.IsValid) // Check if model state is valid
            {
                var userIdClaim = HttpContext.User.FindFirst("UserId");
                int id = int.Parse(userIdClaim.Value);
                PermissionRepo.create(_p,id);
                return RedirectToAction("Index");
            }
            // If model state is not valid, return the view with validation errors
            return View(_p);
        }

        [Authorize(Roles = "Student")]
        public IActionResult Delete(string date)
        {
           PermissionRepo.delete(date);
           return RedirectToAction("Index");
        }

        [Authorize(Roles = "Student")]
        public IActionResult Edit(string date)
        {
            if (date == null)
                return BadRequest();
            var permissionData = PermissionRepo.GetPermissionByDate(date);
            if(permissionData == null)
                return NotFound();
            return View(permissionData);
        }
        [HttpPost]
        [Authorize(Roles = "Student")]
        public IActionResult Edit(Permission permission)
        {

            if (ModelState.IsValid)
            {
                PermissionRepo.edit(permission);
                return RedirectToAction("index");
            }
            return View(permission);
        }

        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        public IActionResult studentPermissions()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);
            var data = PermissionRepo.StdPermissions(id);
            return View("Permissions",data);
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor")]
        public IActionResult ChangeStatus(string date, string status)
        {
            PermissionRepo.ChangeStatus(date,status);
            return RedirectToAction("studentPermissions");
        }



    }
}
