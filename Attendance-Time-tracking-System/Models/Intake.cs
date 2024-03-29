using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.Models
{
    public class Intake
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Length(maximumLength: 50, minimumLength: 5, ErrorMessage = "Enter A name between 5 and 50 letter")]
        public string Name { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<WorksIn> Works { get; set; } = new List<WorksIn>();
        public List<IntakeProgram> intakePrograms { get; set; } = new List<IntakeProgram> ();

    }
}
