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
            return View();
        } 
        public IActionResult Create ()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult create([FromBody]Permission _p)
        {
            PermissionRepo.create(_p);
            return Json("Permission Added Successfully");
        }
    }
}
