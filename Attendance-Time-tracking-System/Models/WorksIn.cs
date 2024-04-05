using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class WorksIn
    {
        [ForeignKey("InstructorNavigation")]
        public virtual int InstructorId { get; set; }
        [ForeignKey("TrackNavigation")]
        public virtual  int TrackId { get; set; }
        [ForeignKey("IntakeNavigation")]
        public virtual int IntakeId { get; set; }
        [ForeignKey("InstructorId")]
        public virtual Instructor InstructorNavigation { get; set; }
        [ForeignKey("TrackId")]
        public virtual Track TrackNavigation { get; set; }
        [ForeignKey("IntakeId")]
        public virtual Intake IntakeNavigation  { get; set; }
    }
}
