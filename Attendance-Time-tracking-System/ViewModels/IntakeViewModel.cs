using Attendance_Time_tracking_System.Models; // Add this using directive if Program class is defined in this namespace

namespace Attendance_Time_tracking_System.ViewModels
{
    public class IntakeViewModel
    {
        public int IntakeId { get; set; }
        public int ProgramId { get; set; }
        public int stdNo { get; set; }
        public string IntakeName { get; set; }
        public string ProgramName { get; set; }

    }
}
