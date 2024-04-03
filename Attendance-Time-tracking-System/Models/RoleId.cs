using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.Models
{
    public class RoleId
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Roles> Roles { get; set; } = new List<Roles>();
    }
}


// 1 -> Admin
//2 -> Studetn
//3 -> Instructor
//4 -> Supervisro
//5 -> Security
//6 -> Student-affairs