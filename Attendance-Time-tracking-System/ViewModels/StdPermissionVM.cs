using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.ViewModels
{
    public class StdPermissionVM
    {
        public Permission PermissionInfo { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }

    }
}
