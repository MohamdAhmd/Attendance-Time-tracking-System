using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class IntakeProgram
    {
        [ForeignKey("IntakeNavigation")]
        public int IntakeId { get; set; }
        [ForeignKey("ProgramNavigation")]
        public int ProgramId {  get; set; }

        public virtual Intake IntakeNavigation { get; set; }
        public virtual Program ProgramNavigation { get; set; }
    }
}
