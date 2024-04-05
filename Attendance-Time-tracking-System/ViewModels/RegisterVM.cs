namespace Attendance_Time_tracking_System.ViewModels
{
    public class RegisterVM
    {
        public string Email { get; set; }
        public string F_name { get; set; }
        public string L_name { get; set;}
        public string Password { get; set;}
        public string Intake { get; set; }
        public string University { get; set; }
        public string specialization { get; set; }
        public DateTime GraduationYear { get; set; }
        public string? ConfirmPassword { get; set; }
        public int phone {  get; set; }

        public int TrackId { get; set; }
        public int IntakeID { get; set; }

    }
}
