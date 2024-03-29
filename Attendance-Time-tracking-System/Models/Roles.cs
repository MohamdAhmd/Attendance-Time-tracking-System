using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class Roles
    {
        [ForeignKey("UserNavigation")]
        public int UserId { get; set; }

        //role can be (Student, Instructor, Supervisor, Security, Student_affairs, Admin)
        public string Role { get; set; }

        public User UserNavigation { get; set; }
    }
}
