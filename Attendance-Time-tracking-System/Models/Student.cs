using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class Student : User
    {
        [Required]
        public string Faculty { get; set; }
        [Required]
        public string University { get; set; }
        [Required]
        public string specialization { get; set; }
        
        public DateTime GraduationYear { get; set; }
        public int Grade { get; set; } 
        // status can be  (Pending, Accepted, Rejected, Fired)
        public string status {  get; set; }
        [Range(5,30,ErrorMessage ="minus shouldn't exseed 30 degree")]
        public int? NextMinus { get; set; }
        [ForeignKey("TrackNavigation")]
        public int TrackId { get; set; }
        [ForeignKey("IntakeNavigation")]
        public int IntakeID { get; set; }

        public virtual Track TrackNavigation { get; set; }
        public virtual Intake IntakeNavigation { get; set; }

    }
}
