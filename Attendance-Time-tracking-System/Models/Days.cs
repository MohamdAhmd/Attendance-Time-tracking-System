using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.Models
{
    public class Days
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime Day { get; set; }

        //can only be (online, offline)
        [Required]
        public string Status { get; set; } 

        public List<Attend> attends { get; set; } = new List<Attend>();
        public List<TrackDays> trackDays { get; set; } = new List<TrackDays>();
    }
}
