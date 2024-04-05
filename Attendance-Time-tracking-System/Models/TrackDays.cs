using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class TrackDays
    {
        [ForeignKey("DayNavigation")]
        public virtual int DayId { get; set; }
        [ForeignKey("TrackNavigation")]
        public virtual int TrackId { get; set; }
        public string? Lecture1 { get; set; }
        public string? Lecture2 { get; set; }
        public string? Lecture3 { get; set; }
        [ForeignKey("DayId")]
        public  virtual Days DayNavigation { get; set; }
        [ForeignKey("TrackId")]
        public virtual Track TrackNavigation { get; set; }
    }
}
