using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class TrackDays
    {
        [ForeignKey("DayNavigation")]
        public int DayId { get; set; }
        [ForeignKey("TrackNavigation")]
        public int TrackId { get; set; }
        [Required]
        public DateTime StartPeriod { get; set; }

        //online offline
        public string Status { get; set; }

        public string? Lecture1 { get; set; }
        public string? Lecture2 { get; set; }
        public string? Lecture3 { get; set; }
        [ForeignKey("DayId")]
        public Days DayNavigation { get; set; }
        [ForeignKey("TrackId")]
        public Track TrackNavigation { get; set; }
    }
}
