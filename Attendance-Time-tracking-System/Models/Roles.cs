using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class Roles
    {
        [ForeignKey("UserNavigation")]
        public int UserId { get; set; }

        //role can be (Student, Instructor, Supervisor, Security, Student_affairs, Admin)
        [ForeignKey("RoleNavigation")]
        public int RoleId { get; set; }
        [ForeignKey("UserId")]
        public User UserNavigation { get; set; }
        [ForeignKey("RoleId")]
        public RoleId RoleNavigation { get; set; }

    }
}
