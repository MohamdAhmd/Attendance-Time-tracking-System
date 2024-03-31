using Attendance_Time_tracking_System.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class Student : User
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "enter a string between 3 and 50")]
        public string Faculty { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "enter a string between 3 and 50")]
        public string University { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "enter a string between 3 and 50")]
        public string specialization { get; set; }
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
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

        public Track TrackNavigation { get; set; }
        public Intake IntakeNavigation { get; set; }

    }
}
