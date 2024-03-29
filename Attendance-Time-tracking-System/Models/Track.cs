using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class Track
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Status { get; set; } = true;
        [ForeignKey("InstructorNavigation")]
        public int SupervisorID { get; set; }
        [ForeignKey("ProgramNavigation")]
        public int ProgramID { get; set; }
        public int? Capacity { get; set; }

        public Instructor InstructorNavigation { get; set; }
        public Program ProgramNavigation { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<WorksIn> Works { get; set; } = new List<WorksIn>();
    }
}
