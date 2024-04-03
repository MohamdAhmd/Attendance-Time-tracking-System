using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.Models
{
    public class RoleId
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }

        public List<Roles> Roles { get; set; } = new List<Roles>();
    }
}
