using Attendance_Time_tracking_System.Validations;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode =true)]
        [Remote("GraduationYear","Validation",ErrorMessage = "Enter a date in the past")]
        public DateTime GraduationYear { get; set; }
        [Display(Name = "Graduation Grade")]
        public string GraduationDegree { get; set; }
        public int Grade { get; set; } 
        // status can be  (Pending, Accepted, Rejected, Fired)
        public string status { get; set; } = "Pending";
        [Range(5, 30, ErrorMessage = "minus shouldn't exseed 30 degree")]
        public int NextMinus { get; set; } 
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
