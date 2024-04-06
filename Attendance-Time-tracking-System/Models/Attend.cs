using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class Attend
    {
        [ForeignKey("UserNavigation")]
        public int UserId { get; set; }
        [ForeignKey("DaysNavigation")]
        public int DayId { get; set; }
        public DateTime? Time { get; set; }
        //(Late , Absent , Present)
        public bool Status { get; set; }
        public bool StatusOut {  get; set; }
        [MaxLength(1)]
        //L : Late  ,,,,, A : Absent
        public string? PermissionType {  get; set; }
        public string? PermissionBody { get; set; }
        //true ,  false
        [DefaultValue(true)]
        public bool? PermissionStatus { get; set; } = true;
        [ForeignKey("UserId")]
        public User UserNavigation { get; set; }
        [ForeignKey("DayId")]
        public Days DaysNavigation { get; set; }
    }
}
