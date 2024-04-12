using Microsoft.CodeAnalysis.Elfie.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class Permission
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime day { get; set; }
        [ForeignKey("StudentNavigation")]
        public int StudentId { get; set; }

        //Late , Absent
        public string? PermissionType { get; set; }
        public string? PermissionBody { get; set; }

        //Pending , Accepted , Rejected
        public string? PermissionStatus { get; set; } = "Pending";

        [ForeignKey("StudentId")]
        public Student StudentNavigation { get; set; }
    }
}
