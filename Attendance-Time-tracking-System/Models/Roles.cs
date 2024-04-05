using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class Roles
    {
        [ForeignKey("UserNavigation")]
        public virtual int UserId { get; set; }

        //role can be (Student, Instructor, Supervisor, Security, Student_affairs, Admin)
        [ForeignKey("RoleNavigation")]
        public virtual int RoleId { get; set; }
        [ForeignKey("UserId")]
        public virtual User UserNavigation { get; set; }
        [ForeignKey("RoleId")]
        public virtual RoleId RoleNavigation { get; set; }

    }
}
