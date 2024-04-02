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
        public string? Status { get; set; }
        public string? PermissionType {  get; set; }
        public string? PermissionBody { get; set; }
        public string? PermissionStatus { get; set; }

        public virtual User UserNavigation { get; set; }

        public virtual Days DaysNavigation { get; set; }
    }
}
