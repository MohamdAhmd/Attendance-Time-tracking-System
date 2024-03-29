using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class WorksIn
    {
        [ForeignKey("InstructorNavigation")]
        public  int InstructorId { get; set; }
        [ForeignKey("TrackNavigation")]
        public int TrackId { get; set; }
        [ForeignKey("IntakeNavigation")]
        public int IntakeId { get; set; }

        public Instructor InstructorNavigation { get; set; }
        public Track TrackNavigation { get; set; }
        public Intake IntakeNavigation  { get; set; }
    }
}
