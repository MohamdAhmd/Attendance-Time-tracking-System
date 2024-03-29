using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class IntakeProgram
    {
        [ForeignKey("IntakeNavigation")]
        public int IntakeId { get; set; }
        [ForeignKey("ProgramNavigation")]
        public int ProgramId {  get; set; }

        public Intake IntakeNavigation { get; set; }
        public Program ProgramNavigation { get; set; }
    }
}
