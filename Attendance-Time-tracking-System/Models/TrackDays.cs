using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class TrackDays
    {
        [ForeignKey("DayNavigation")]
        public int DayId { get; set; }
        [ForeignKey("TrackNavigation")]
        public int TrackId { get; set; }
        public string? Lecture1 { get; set; }
        public string? Lecture2 { get; set; }
        public string? Lecture3 { get; set; }
        public Days DayNavigation { get; set; }
        public Track TrackNavigation { get; set; }
    }
}
