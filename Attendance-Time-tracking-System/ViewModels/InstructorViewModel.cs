namespace Attendance_Time_tracking_System.ViewModels
{
    public class InstructorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Salary { get; set; }
        public DateTime? HireDate { get; set; }
        public bool Status { get; set; }
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

    }
}
