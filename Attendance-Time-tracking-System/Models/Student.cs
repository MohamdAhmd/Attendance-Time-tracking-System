using Attendance_Time_tracking_System.Validations;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Mvc;
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
        public string status { get; set; } = "Pending";
        [Range(5, 30, ErrorMessage = "minus shouldn't exseed 30 degree")]
        public virtual int NextMinus { get; set; } 
        [ForeignKey("TrackNavigation")]
        public virtual int TrackId { get; set; }
        [ForeignKey("IntakeNavigation")]
        public virtual int IntakeID { get; set; }
        [ForeignKey("TrackId")]
        public virtual Track TrackNavigation { get; set; }
        [ForeignKey("IntakeID")]
        public virtual Intake IntakeNavigation { get; set; }

    }
}
