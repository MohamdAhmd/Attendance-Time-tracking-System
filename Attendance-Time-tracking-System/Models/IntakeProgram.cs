using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class IntakeProgram
    {
        [ForeignKey("IntakeNavigation")]
        public int IntakeId { get; set; }
        [ForeignKey("ProgramNavigation")]
        public int ProgramId {  get; set; }
        [ForeignKey("IntakeId")]
        public Intake IntakeNavigation { get; set; }
        [ForeignKey("ProgramId")]
        public Program ProgramNavigation { get; set; }
    }
}
