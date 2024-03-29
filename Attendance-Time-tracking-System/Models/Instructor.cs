namespace Attendance_Time_tracking_System.Models
{
    public class Instructor : User
    {
        public int Salary { get; set; }
        public DateTime? HireDate { get; set; }

        public List<WorksIn> works { get; set; } = new List<WorksIn>();
    }
}
