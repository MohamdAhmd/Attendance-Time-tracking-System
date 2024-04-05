using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.Models
{
    public class Instructor : User
    {
        [Range(5000, 50000, ErrorMessage = "Salary between 5000 and 50000")]
        public int Salary { get; set; }
        public DateTime? HireDate { get; set; }

        public virtual List<WorksIn> works { get; set; } = new List<WorksIn>();
        public virtual Track? supervisor { get; set; } 
    }
}
