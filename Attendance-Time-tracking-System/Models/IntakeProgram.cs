using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class IntakeProgram
    {
        [ForeignKey("IntakeNavigation")]
        public virtual int IntakeId { get; set; }
        [ForeignKey("ProgramNavigation")]
        public virtual int ProgramId {  get; set; }
        [ForeignKey("IntakeId")]
        public virtual Intake IntakeNavigation { get; set; }
        [ForeignKey("ProgramId")]
        public virtual Program ProgramNavigation { get; set; }
    }
}
