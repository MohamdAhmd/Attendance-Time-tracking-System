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
        [ForeignKey("InstructorId")]
        public Instructor InstructorNavigation { get; set; }
        [ForeignKey("TrackId")]
        public Track TrackNavigation { get; set; }
        [ForeignKey("IntakeId")]
        public Intake IntakeNavigation  { get; set; }
    }
}
